using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Auth.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        public NameController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }


        // GET: <NameController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "peraKojotSuperGenije", "SimaSimic" };
        }

        // GET <NameController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [AllowAnonymous]
        [HttpGet("authenticate")]
        public IActionResult Authenticate([FromQuery] UserCred userCred )
        {
            var token = _jwtAuthenticationManager.Authenticate(userCred.username, userCred.password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }

    public class UserCred
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
