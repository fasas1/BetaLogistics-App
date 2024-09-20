using BetaLogistics.Models.Dto;

namespace BetaLogistics.Repository.IRepository
{
    public interface IAuthRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<UserDto> Register(RegistrationRequestDto registerationRequestDto);
        Task<bool> AssignRole(string userName, string roleName);
    }
}
