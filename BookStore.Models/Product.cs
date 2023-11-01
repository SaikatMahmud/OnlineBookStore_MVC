using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(1, 1000)]
        [Display(Name = "List Price")]
       
        public double ListPrice { get; set; }

        [Required]
        [Range(1, 1000)]
        [Display(Name = "Price for 1-50")]
        public double Price { get; set; }

        [Range(1, 1000)]
        [Required]
        [Display(Name = "Price for 50+")]
        public double Price50 { get; set; }

        [Range(1, 1000)]
        [Required]
        [Display(Name = "Price for 100+")]
        public double Price100 { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        public string? ImageUrl { get; set; }

    }
}
