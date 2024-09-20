using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetaLogistics.Models
{
    public class LocationUpdate
    {

        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationId { get; set; }
        public int ShipmentId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public Shipment Shipment { get; set; }
    }
}
