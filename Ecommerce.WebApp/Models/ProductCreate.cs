using System.ComponentModel.DataAnnotations;

namespace Ecommerce.WebApp.Models
{
    public class ProductCreate
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public string Price { get; set; }
        [Required]
        public string Category { get; set; }
    }
}
