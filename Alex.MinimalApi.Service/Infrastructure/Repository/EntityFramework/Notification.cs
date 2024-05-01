using System.ComponentModel.DataAnnotations;

namespace Alex.MinimalApi.Service.Infrastructure.Repository.EntityFramework
{
    public class Notification : RepositoryEntity
    {
        [Required]
        [StringLength(500)]
        public required string Message { get; set; }
    }
}
