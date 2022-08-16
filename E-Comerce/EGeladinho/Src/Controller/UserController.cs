using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EGeladinho.Src.Models;
using EGeladinho.Src.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EGeladinho.Src.Controller
{
    [ApiController]
    [Route("api/Users")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly ICrud<User> _repository;

        public UserController(ICrud<User> repository){
            _repository = repository;
        }
        
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User user)
        {
            await _repository.Create(user);

            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var user = await _repository.Read(id);
            if(user == null){
                return NotFound();
            }
            return Ok(id);
        }
        
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] User user)
        {
            try
            {
                await _repository.Update(user);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _repository.Delete(id);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}