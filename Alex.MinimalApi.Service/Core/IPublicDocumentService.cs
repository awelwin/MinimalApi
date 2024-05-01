namespace Alex.MinimalApi.Service.Core
{
    /// <summary>
    /// Document processing
    /// </summary>
    public interface IPublicDocumentService
    {
        /// <summary>
        /// Persist document to 3rd party storage
        /// </summary>
        /// <param name="content">document content</param>
        /// <returns>unique document id</returns>
        Task<string> CreateAsync(System.IO.Stream content);
    }
}
