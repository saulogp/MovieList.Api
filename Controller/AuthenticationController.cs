using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieList.Repositories;
using MovieList.Services;
using MovieList.ViewModels;

namespace MovieList.Controller
{
    [ApiController]
    [Route("v1")]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> AuthenticationAsync([FromBody] LoginModel model){
            var user = UserRepository.Get(model.Username, model.Password);

            if(user == null)
                return BadRequest("User invalid");
            
            var token = TokenServices.GenerateToken(user);
            user.Password = "";
            
            return Ok(new{
                user = user,
                token = token    
            });
        }
        
    }
}