using Data.DataModels;
using DomainModels;
using Helpers.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCreamTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly JwtHandler _jwtHandler;
        private readonly UserRepository _userRepository;
        private Data.DataModels.IceCreamDataContext _context;
        public AccountController(JwtHandler jwtHandler, IceCreamDataContext context, UserRepository userRepository)
        {
            _context = context;
            _jwtHandler = jwtHandler;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("externallogin")]
        public async Task<IActionResult> ExternalLogin(ExternalAuth externalAuth)
        {
            try
            {
                var payload = await _jwtHandler.VerifyGoogleToken(externalAuth);
                if(payload == null)
                {
                    return BadRequest("Invalid External Authentication");
                }
                
                return this.Ok(await this._userRepository.GetUserLogin(payload.Subject));   
            }
            catch(Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
     
    }
}
