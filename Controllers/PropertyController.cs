using Airbnb.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Airbnb.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        PropertyDBAccess propertydbAccess=new PropertyDBAccess();

        public PropertyController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/<PropertyController>
        [HttpGet]
        public IEnumerable<PropertyFields> GetpropertyList()
        {
            string myconnectionstring = _configuration["ConnectionStrings:myconnectionstring"];
            List<PropertyFields> propertyFields = new List<PropertyFields>();

            return propertyFields = propertydbAccess.GetPropertyList(myconnectionstring);

                //Added comment
        }

        // GET api/<PropertyController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PropertyController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PropertyController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PropertyController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
