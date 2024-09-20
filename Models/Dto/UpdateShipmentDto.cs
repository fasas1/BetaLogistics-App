using BetaLogistics.Models;
using System.ComponentModel.DataAnnotations;

namespace BetaLogistics.Models.Dto
{
    public class UpdateShipmentDto
    {
        public int ShipmentId { get; set; }
        [Required]
        public string TrackingNumber { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public string Status { get; set; }
        public DateTime EstimatedDelivery { get; set; }
        
    }
}
