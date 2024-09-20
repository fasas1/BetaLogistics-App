using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetaLogistics.Models.Dto
{
    public class VehicleDto
    {
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public string LicensePlate { get; set; }
        [Required]
        public string Model { get; set; }
        public ICollection<Driver> Drivers { get; set; }
    }
}
