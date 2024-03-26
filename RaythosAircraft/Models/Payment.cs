using System.ComponentModel.DataAnnotations;

namespace RaythosAircraft.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public string cardName { get; set; }
        public string cardNumber { get; set; }
        public string cardCVC { get; set; }
        public string cardExpiry { get; set; }
        public string paymentStatus { get; set; }
        public DateTime paymentDate { get; set; }

    }
}
