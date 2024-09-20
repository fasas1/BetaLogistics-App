using BetaLogistics.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetaLogistics.Models
{
    public class Driver
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DriverId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LicenseNumber { get; set; }
        public int VehicleId { get; set; }
        public Vehicle  Vehicle  { get; set; }
        public ICollection<Shipment> Shipments { get; set; }
    }
}
