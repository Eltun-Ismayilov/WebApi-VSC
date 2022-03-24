using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.DTOs.User;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController:ControllerBase
    {
        private readonly IAuthRepository repo;

        public AuthController(IAuthRepository repo)
        {
            this.repo = repo;
        }
        
        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var respons=await repo.Register(new User{Username=request.Username},request.Password);
            if(!respons.Success) return BadRequest(respons);
            return Ok(respons);
        }
                
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginDto request)
        {
            var respons=await repo.Login(request.Username,request.Password);
            if(!respons.Success) return BadRequest(respons);
            return Ok(respons);
        }
    }
}