using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System.Diagnostics;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int addressId = await _addressService.CreateAddressAsync(address);
                return CreatedAtAction(nameof(GetAddressAsync), new { id = addressId }, address);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the address.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressAsync(int id)
        {
            try
            {
                var address = await _addressService.GetAddressAsync(id);
                if (address == null) return NotFound();
                return Ok(address);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the address.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddressAsync(int id, [FromBody] Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != address.AddressID)
            {
                return BadRequest("Address Id mismatch");
            }
            try
            {
                await _addressService.UpdateAddressAsync(address);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the address.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddressAsync(int id)
        {
            try
            {
                await _addressService.DeleteAddressAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the address.");
            }
        }
    }
}