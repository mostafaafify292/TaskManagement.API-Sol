using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Dtos;
using TaskManagement.Domain.Repository.Contract;
using TaskManagement.Repository;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAuthToken _authToken;

        public AuthController(UserManager<IdentityUser> userManger , SignInManager<IdentityUser> signInManager , IAuthToken authToken) 
        {
            _userManger = userManger;
            _signInManager = signInManager;
            _authToken = authToken;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register (/*[FromRoute]*/ RegisterDTO registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input data.");
            }
            if (CheckEmailExists(registerDto.Email).Result.Value)
            {
                return BadRequest("this email is already exist!");
            }
            var user = new IdentityUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };
            var result = await _userManger.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            // assign a default role
            await _userManger.AddToRoleAsync(user, "User");

            return Ok(new UserDto()
            {
                DisplayName = user.UserName,
                Email = user.Email,
                Token = "login"
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManger.FindByEmailAsync(loginDto.email);
            if (user == null) return Unauthorized("Invalid username or password.");
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Invalid username or password.");
            }
            return Ok(new UserDto()
            {
                DisplayName = user.UserName,
                Email = user.Email,
                Token = await _authToken.CreateTokenAsync(user, _userManger)
            });

        }

        [HttpGet("emailExist")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManger.FindByEmailAsync(email) is not null;
        }
    }
}
