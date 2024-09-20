using Microsoft.AspNetCore.Identity;

namespace BetaLogistics.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string Name { get; set; }
    }
}
