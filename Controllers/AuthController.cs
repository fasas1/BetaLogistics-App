using Asp.Versioning;
using BetaLogistics.Models;
using BetaLogistics.Models.Dto;
using BetaLogistics.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BetaLogistics.Controllers
{
    [Route("api/v/Auth")]
    [ApiController]
    [ApiVersionNeutral]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        protected APIResponse _response;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
            _response = new();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authRepo.Login(model);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username or password is incorrect");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            bool ifUserNameUnique = _authRepo.IsUniqueUser(model.UserName);
            if (!ifUserNameUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username already exists");
                return BadRequest(_response);
            }

            var user = await _authRepo.Register(model);
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Error while registering");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccessful = await _authRepo.AssignRole(model.UserName, model.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                _response.IsSuccess = false;
                //_response.ErrorMessages = "Error encountered";
                return BadRequest(_response);
            }
            return Ok(_response);

        }
    }
}
