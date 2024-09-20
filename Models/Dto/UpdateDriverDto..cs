using System.ComponentModel.DataAnnotations;

namespace BetaLogistics.Models.Dto
{
    public class UpdateDriverDto
    {
        [Required]
        public int DriverId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LicenseNumber { get; set; }
  
    }
}
