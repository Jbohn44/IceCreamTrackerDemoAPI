using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DataModels;
using DomainModels;
using DomainModels.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using IceCreamTrackerApi.Attributes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace IceCreamTrackerApi.Controllers
{
    [GoogleAuthAttr]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserRepository _userRepository;
        private IceCreamDataContext context;

        public UserController(IceCreamDataContext _context, UserRepository userRepository)
        {
            context = _context;
            _userRepository = userRepository;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost("signup")]
        public async Task<ActionResult<DomainModels.User>> UserSignUp(DomainModels.User user)
        {
            try
            {
                return this.Ok(await _userRepository.CreateUser(user));
            }
            catch(Exception ex) 
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
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

      

        [HttpGet("getall")]
        public async Task<ActionResult<List<string>>> GetAllUsernames()
        {
            try
            {
                return this.Ok(await _userRepository.GetUserNames());
            }
            catch(Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

    }
}
