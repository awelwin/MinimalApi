using System.ComponentModel.DataAnnotations;

namespace Alex.MinimalApi.Service.Repository.EntityFramework
{
    public class Notification : RepositoryEntity
    {
        [Required]
        [StringLength(500)]
        public string Message { get; set; }
    }
}
