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
    [Route("api/Carts")]
    [Produces("application/json")]
    public class CartController : ControllerBase
    {
        private readonly ICrud<Cart> _repository;

        public CartController(ICrud<Cart> repository){
            _repository = repository;
        }
        
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Cart cart)
        {
            await _repository.Create(cart);

            return Ok(cart);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var user = await _repository.Read(id);
            if(user == null){
                return NotFound();
            }
            return Ok(id);
        }
        
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Cart cart)
        {
            try
            {
                await _repository.Update(cart);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("id/{id}")]
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