using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    public class BaseEntity
    {
        [Required]
        public int Id { get; set; }
    }
}
