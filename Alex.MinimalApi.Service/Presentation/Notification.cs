using System.ComponentModel.DataAnnotations;

namespace Alex.MinimalApi.Service.Presentation
{
    public class Notification : PresentationEntity
    {
        /// <summary>
        /// content of the notification
        /// </summary>
        [StringLength(500)]
        [Required]
        public required string Message { get; set; }
    }
}
