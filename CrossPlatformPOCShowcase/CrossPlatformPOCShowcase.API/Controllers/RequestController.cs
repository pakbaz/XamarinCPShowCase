using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CrossPlatformPOCShowcase.Core.Interfaces;
using CrossPlatformPOCShowcase.Core.Models;
using System.Threading.Tasks;

namespace CrossPlatformPOCShowcase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IDataStore<Request> _requestRepository;

        public RequestController(IDataStore<Request> itemRepository)
        {
            _requestRepository = itemRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Request>>> List()
        {
            var items = await _requestRepository.GetItemsAsync(true);
            return Ok(items);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Request>> GetItem(string id)
        {
            Request item = await _requestRepository.GetItemAsync(id);

            if (item == null)
                return NotFound();

            return item;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Item>> Create([FromBody]Request item)
        {
            await _requestRepository.AddItemAsync(item);
            return CreatedAtAction(nameof(GetItem), new { item.id }, item);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Edit([FromBody] Request item)
        {
            try
            {
                await _requestRepository.UpdateItemAsync(item);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(string id)
        {
            bool result = await _requestRepository.DeleteItemAsync(id);

            if (result)
                return Ok();
            else
                return NotFound();
        }
    }
}
