using AutoMapper;
using Azure;
using BetaLogistics.Data;
using BetaLogistics.Models;
using BetaLogistics.Models.Dto;
using BetaLogistics.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace BetaLogistics.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly string _secretKey;
        private readonly IMapper _mapper;

        public AuthRepository(ApplicationDbContext db, IConfiguration configuration,
            UserManager<ApplicationUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
            _secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            _roleManager = roleManager;
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(x => x.UserName == username);
            return user == null;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

            if (user != null)
            {
                // Check if roles exist, create them if they don't
                if (!await _roleManager.RoleExistsAsync(SD.Role_Admin))
                {
                    await _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
                }

                if (!await _roleManager.RoleExistsAsync(SD.Role_Customer))
                {
                    await _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer));
                }

                // Assign role to user
                if (roleName.ToLower() == SD.Role_Admin.ToLower())
                {
                    await _userManager.AddToRoleAsync(user, SD.Role_Admin);
                }
                else if (roleName.ToLower() == SD.Role_Customer.ToLower())
                {
                    await _userManager.AddToRoleAsync(user, SD.Role_Customer);
                }
                else
                {
                    // Handle the case when the role name doesn't match any known roles
                    return false;
                }

                return true; // Role assignment successful
            }

            return false; // User not found
        }



        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicationUsers
                .FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            if (user == null)
            {
                return new LoginResponseDto { Token = "", User = null };
            }

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (!isValid)
            {
                return new LoginResponseDto { Token = "", User = null };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, roles.FirstOrDefault() ?? string.Empty)
        }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var loginResponseDTO = new LoginResponseDto
            {
                Token = tokenHandler.WriteToken(token),
                User = _mapper.Map<UserDto>(user)  // AutoMapper will now be able to map
            };
            return loginResponseDTO;
        }


        public async Task<UserDto> Register(RegistrationRequestDto registrationRequestDto)
        {
            var user = new ApplicationUser
            {
                UserName = registrationRequestDto.UserName,
                Email = registrationRequestDto.UserName,
                NormalizedEmail = registrationRequestDto.UserName.ToUpper(),
                Name = registrationRequestDto.Name
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDto.UserName);

                    UserDto userDto = new()
                    {
                        UserName = userToReturn.Email,
                        ID = userToReturn.Id,
                        Name = userToReturn.Name,
                       // PhoneNumber = userToReturn.PhoneNumber
                    };

                    return userDto;
                }
            }
            catch (Exception e)
            {
                // Log exception or handle it appropriately
                throw new Exception("An error occurred while registering the user", e);
            }

            return new UserDto();
        }
    }
}
