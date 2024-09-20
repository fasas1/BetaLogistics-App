using System.ComponentModel.DataAnnotations;

namespace BetaLogistics.Models.Dto
{
    public class CreateDriverDto
    {
       
        [Required]
        public string Name { get; set; }
        [Required]
        public string LicenseNumber { get; set; }

      
    }
}
