using System.ComponentModel.DataAnnotations;

namespace BetaLogistics.Models.Dto
{
    public class CreateVehicleDto
    {
        [Required]
        public int VehicleId { get; set; }
        public string LicensePlate { get; set; }
        [Required]
        public string Model { get; set; }
    }
}
