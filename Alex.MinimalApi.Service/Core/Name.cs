namespace Alex.MinimalApi.Service.Core
{
    //Encapsulate full name
    public class Name
    {
        /// <summary>
        /// firstname
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Lastname
        /// </summary>
        public string Lastname { get; set; }

        ///<summary>
        /// Title e.g. Mr, Ms
        /// </summary>
        public string title { get; set; }
    }
}
