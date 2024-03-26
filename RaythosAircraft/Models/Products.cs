using System.ComponentModel.DataAnnotations;

namespace RaythosAircraft.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string ProductCode { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Img_1 { get; set; }

        public string Img_2 { get; set; }

        [Required]
        public double Basic_Price { get; set; }

        public string Specifications { get; set; }

        public string Description { get; set; }

        public int Days { get; set; }

        public int Quantity { get; set; }
    }

}
