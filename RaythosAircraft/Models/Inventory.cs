using System.ComponentModel.DataAnnotations;

namespace RaythosAircraft.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }

        [Required]
        public string ItemCode { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Img1 { get; set; }
        public Double Price { get; set; }
        public string Specifications { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }
        public string Quality { get; set; }
        public DateTime Days { get; set; }
        public int Quantity { get; set; }

        public static implicit operator Inventory(string v)
        {
            throw new NotImplementedException();
        }
    }
}