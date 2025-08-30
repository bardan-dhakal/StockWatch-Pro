using api.Dtos;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController(IRegisterRepository registerRepository,ITokenService tokenService,UserManager<AppUser> userManager) : ControllerBase
    {
        private readonly IRegisterRepository registerRepository = registerRepository;
        private readonly ITokenService tokenService = tokenService;
        private readonly UserManager<AppUser> userManager = userManager;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
             
            var userModel = new AppUser{
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            var createdUser = await registerRepository.Register(registerDto,userModel);
            if(!createdUser.Succeeded){
                return StatusCode(500,createdUser.Errors);
            }
            
            var roleResult = await registerRepository.AddToRoleAsync(userModel,"User");
            if(!roleResult.Succeeded){
                return StatusCode(500,roleResult.Errors);
            }
             
            return Ok(tokenService.CreateToken(userModel));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var userMail = await userManager.FindByEmailAsync(loginDto.Email!);
            if(userMail == null){
                return Unauthorized("Invalid email");
            }
            var user = await userManager.FindByNameAsync(userMail.UserName!);
            if(user == null){
                return Unauthorized("Invalid username");
            }
            var passwordCheck = await userManager.CheckPasswordAsync(user,loginDto.Password!);
            if(!passwordCheck){
                return Unauthorized("Invalid password");
            }
            return Ok(tokenService.CreateToken(user));
        }
    }
}