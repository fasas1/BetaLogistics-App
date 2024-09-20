using System.ComponentModel.DataAnnotations;

namespace BetaLogistics.Models.Dto
{
    public class CreateCustomerDto
    {      
       
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
