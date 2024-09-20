using System.ComponentModel.DataAnnotations;

namespace BetaLogistics.Models.Dto
{
    public class UpdateVehicleDto
    {
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public string LicensePlate { get; set; }
        [Required]
        public string Model { get; set; }
    }
}
