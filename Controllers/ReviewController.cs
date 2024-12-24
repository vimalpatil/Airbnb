﻿using Airbnb.WebAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Airbnb.WebAPI.Controllers

{
   

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        ReviewDBAccess reviewDBAccess=new ReviewDBAccess();

        public ReviewController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/<ReviewController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ReviewController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ReviewController>
        [HttpPost]
        [EnableCors("MyAllowSpecificOrigins")] // Required for this path.
        public void Post([FromBody] ReviewFields postReviewData)
        {
            string myconnectionstring = _configuration["ConnectionStrings:myconnectionstring"];
            reviewDBAccess.AddReview(postReviewData, myconnectionstring);
        }

        // PUT api/<ReviewController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReviewController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
