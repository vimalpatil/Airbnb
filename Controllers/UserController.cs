using Airbnb.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Airbnb.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        UserDBAccess userDbaccess=new UserDBAccess();

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [EnableCors("MyAllowSpecificOrigins")]
        [HttpGet("getUserDetails")]
        public UserFields Get([FromQuery] string username, [FromQuery] string password)
        {
            string myconnectionstring = _configuration["ConnectionStrings:myconnectionstring"];
            return userDbaccess.GetRecordBy(username, password,myconnectionstring);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [EnableCors("MyAllowSpecificOrigins")]
        [HttpPost]
            public int Post([FromBody] UserFields postUserdata)
        {
            string myconnectionstring = _configuration["ConnectionStrings:myconnectionstring"];
            int userid =  userDbaccess.AddUser(postUserdata, myconnectionstring);
            return userid;
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
