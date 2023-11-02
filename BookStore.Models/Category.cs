using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Range(1, 100, ErrorMessage = "Display Order for category must be between 1 and 100")]
        public int DisplayOrder { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
