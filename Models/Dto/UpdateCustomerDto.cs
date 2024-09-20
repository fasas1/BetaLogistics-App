using System.ComponentModel.DataAnnotations;

namespace BetaLogistics.Models.Dto
{
    public class UpdateCustomerDto
    {
       
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
