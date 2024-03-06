using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaySlipBackend.Context;
using PaySlipBackend.Models;

namespace PaySlipBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _authContext;
        public UserController(AppDbContext appDbContext)
        {
            _authContext = appDbContext;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserDTO user)
        {
            if (user != null)
            {
                var _user = await _authContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password);

                if (_user == null)
                {
                    return NotFound(new { Message = "User Not Found" });
                }
                else if (_user != null) { }
                {
                    Console.WriteLine("login rejected");
                    return Ok(new
                    {
                        Message = "Login Success"
                    });
                }
            }

            else if (user == null) { return BadRequest(); }
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            if (user != null)
            {
                await _authContext.Users.AddAsync(user);
                await _authContext.SaveChangesAsync();
                return Ok(new
                {
                    Message = "User Registered"
                });
            }
            else if (user == null)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
