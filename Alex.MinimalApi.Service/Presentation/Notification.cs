using System.ComponentModel.DataAnnotations;

namespace Alex.MinimalApi.Service.Presentation
{
    public class Notification : PresentationEntity
    {
        /// <summary>
        /// content of the notification
        /// </summary>
        [StringLength(500)]
        public string Message { get; set; }
    }
}
