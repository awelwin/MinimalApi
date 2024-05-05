
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Alex.MinimalApi.Service.Infrastructure.Repository.EntityFramework
{
    /// <summary>
    /// Extensions for DbContext
    /// </summary>
    public static class DbContextExtensions
    {
        public static T DeleteGraph<T>(this DbContext context, T newEntity, T existingEntity) where T : class
        {
            return DeleteGraph(context, newEntity, existingEntity, null!);
        }

        /// <summary>
        /// Get list of objects that represents the Primary Key of an entity
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public static object[] GetPrimaryKeyValues(this EntityEntry entry)
        {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return entry.Metadata.FindPrimaryKey()!
                .Properties
                .Select(p => entry.Property(p.Name).CurrentValue)
                .ToArray();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        /// <summary>
        /// Use Reflection to traverse disconnected entity(updated) and mark connected(existing) for deletion where requried
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context">dbContext</param>
        /// <param name="updatedEntity">disconnected entity with changes</param>
        /// <param name="existingEntity">connected entity with existing values</param>
        /// <param name="aggregateType">aggregate type classname</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        private static T DeleteGraph<T>(this DbContext context, T updatedEntity, T existingEntity, string aggregateType) where T : class
        {
            //Add Aggregate
            if (existingEntity == null)
            {
                context.Add(updatedEntity);
                return updatedEntity;
            }
            //Delete Aggregage
            else if (updatedEntity == null)
            {
                context.Remove(existingEntity);
                return null;
            }
            else
            {
                var existingEntry = context.Entry(existingEntity);
                existingEntry.CurrentValues.SetValues(updatedEntity);

                //Loop Existing Navigation children (Ex aggregate root class)
                foreach (var existNavItem in existingEntry.Navigations.Where(n => n.IsLoaded && n.Metadata.ClrType.FullName != aggregateType))
                {

                    //get corresponding nav from updated
                    string classname = existNavItem.Metadata.Name;
                    var x = existingEntry.Entity.GetType().GetProperty(classname);
                    var updatedNavItem = x!.GetValue(updatedEntity);
                    var updatedNavItemAsEnumerable = updatedNavItem as IEnumerable<object>;

                    // Is Collection ???
                    if (existNavItem.Metadata.IsCollection)
                    {
                        //Convert to IEnumerable
                        if (!(existNavItem.CurrentValue is IEnumerable<object> existingNavAsEnumerable))
                            throw new NullReferenceException($"Couldn't iterate through the DB value of the Navigation '{existNavItem.Metadata.Name}'");

                        //Loop Values
                        foreach (var existChild in existingNavAsEnumerable.ToList())
                        {
                            //child id
                            var existChildId = context.Entry(existChild).GetPrimaryKeyValues();

                            //deleted ???
                            bool deleted = false;
                            if (updatedNavItemAsEnumerable != null)
                                if (updatedNavItemAsEnumerable.All(v => !context.Entry(v).GetPrimaryKeyValues().SequenceEqual(existChildId)))
                                    deleted = true;

                            if (updatedNavItem == null)
                                deleted = true;

                            //execute / track delete
                            if (deleted)
                            {
                                //get method to delete
                                var deleteMethod = existingNavAsEnumerable.GetType().GetMethod("Remove");

                                if (deleteMethod == null)
                                    throw new NullReferenceException($"The collection type in the Navigation property '{existNavItem.Metadata.Name}' doesn't have a 'Remove' method.");

                                //EXECUTE - Remove from collection
                                deleteMethod.Invoke(existingNavAsEnumerable, new[] { existChild });
                            }
                        }
                    }
                    else
                    {
                        // the navigation is not a list
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
                        DeleteGraph(context, updatedNavItem, existNavItem.CurrentValue, existingEntry.Metadata.ClrType.FullName!);
#pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
                    }
                }//--loop

                return existingEntity;
            }
        }
    }
}
