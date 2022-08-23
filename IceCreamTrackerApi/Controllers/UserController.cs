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
using Repository.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace IceCreamTrackerApi.Controllers
{
    [GoogleAuthAttr]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
