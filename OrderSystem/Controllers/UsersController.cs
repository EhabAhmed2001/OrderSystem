using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Core.Entities;
using OrderSystem.Core.Service;
using OrderSystem.PL.DTOs;

namespace OrderSystem.PL.Controllers
{
    public class UsersController : APIBaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _token;

        public UsersController(UserManager<User> userManager, ITokenService token)
        {
            _userManager = userManager;
            _token = token;
        }

        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDto registerDto)
        {
            var UserExist = await _userManager.FindByEmailAsync(registerDto.Email);
            if (UserExist is not null)
                return BadRequest(new { Message = "This Email Is Already Exist" });

            var user = new User()
            {
                Email = registerDto.Email,
                UserName = registerDto.Email.Split('@')[0],
                Role = registerDto.Role                
            };
            var Result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!Result.Succeeded) 
                return BadRequest(new { Message = "An Error Happened.. Try Again" });

            var ReturnedUser = new UserDto()
            {
                UserName = registerDto.Email.Split('@')[0],
                Email = registerDto.Email,
                Role = registerDto.Role,
                Token = _token.CreateTokenAsync(user)
            };

            return Ok(ReturnedUser);
        }


       [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null)
                return Unauthorized(new { Message = "Email Or Password Is Wrong" });

            var Result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!Result)
                return Unauthorized(new { Message = "Email Or Password Is Wrong" });

            var ReturnedUser = new UserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
                Token = _token.CreateTokenAsync(user)
            };

            return Ok(ReturnedUser);
        }
    }
}
