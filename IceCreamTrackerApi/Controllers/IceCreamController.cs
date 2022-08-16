using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DomainModels;
using Microsoft.AspNetCore.Http;
using Repository;
using Data.DataModels;
using IceCreamTrackerApi.Attributes;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Repository.Interfaces;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IceCreamTrackerApi.Controllers
{
    [GoogleAuthAttr]   
    [Route("api/[controller]")]
    [ApiController]
    public class IceCreamController : ControllerBase
    {
        private IIceCreamRepository _iceCreamRepository;
        public IceCreamController( IIceCreamRepository iceCreamRepository)
        {
            _iceCreamRepository = iceCreamRepository;
        }
        // GET: api/<IceCreamController>
        [HttpGet("datafeed/{id}")]
        public async Task<ActionResult<List<DomainModels.IceCream>>> GetDataFeed(int id)
        {
            try
            {
                return this.Ok(await _iceCreamRepository.getDataFeed(id));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET api/<IceCreamController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<DomainModels.IceCream>>> GetList(int id)
        {
            try
            {
                return this.Ok(await _iceCreamRepository.getIceCreams(id));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        //Get Single Detail
        [HttpGet("detail/{id:int}")]
        public async Task<ActionResult<DomainModels.IceCream>> GetSingle(int id)
        {
            try
            {
                return this.Ok(await _iceCreamRepository.getSingleIceCream(id));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);

            }
        }

        // POST api/<IceCreamController>
        [HttpPost("post")]
        public async Task<ActionResult<DomainModels.IceCream>> Post(DomainModels.IceCream iceCream)
        {
            try
            {
                return this.Ok(await _iceCreamRepository.postIceCream(iceCream));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // PUT api/<IceCreamController>/5
        [HttpPut("put")]
        public async Task<ActionResult<DomainModels.IceCream>> Put(DomainModels.IceCream iceCream)
        {
            try
            {
                return this.Ok(await _iceCreamRepository.UpdateIceCream(iceCream));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // DELETE api/<IceCreamController>/5
        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _iceCreamRepository.Delete(id);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /*
         * @Params: int id - userid to filter out results
         * returns list of reviews other users have made
         * 
         */
        [HttpGet("user/reviews/{id:int}")]
        public async Task<ActionResult> GetOtherReviews(int id)
        {
            try
            {
                return this.Ok(await _iceCreamRepository.GetOtherUserReviews(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
