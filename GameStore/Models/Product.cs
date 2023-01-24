using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    public class Product : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int AgeRestriction { get; set; }
        [Required]
        public string? Company { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
