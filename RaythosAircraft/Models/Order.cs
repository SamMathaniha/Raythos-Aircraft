using System.ComponentModel.DataAnnotations;

namespace RaythosAircraft.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string ContactNo { get; set; }
        [Required]
        public string AgreementStatus { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public double Total {  get; set; }

        [Required]
        //Foreign Keys
        public string ProductName { get; set; }

        public static implicit operator Order(List<Order> v)
        {
            throw new NotImplementedException();
        }
    }
}
