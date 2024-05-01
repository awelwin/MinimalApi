using FluentValidation;

namespace Alex.MinimalApi.Service.Application.EndpointFilters
{

    public class ValidationEndpointFilter<T> : IEndpointFilter
    {
        async ValueTask<object?> IEndpointFilter.InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
            if (validator != null)
            {
                var entity = context.Arguments.OfType<T>().FirstOrDefault(a => a?.GetType() == typeof(T));
                if (entity != null)
                {
                    var validation = await validator.ValidateAsync(entity);
                    if (validation.IsValid)
                    {
                        return await next(context);
                    }
                    return Results.ValidationProblem(validation.ToDictionary());
                }
            }
            return await next(context);

        }
    }
}
