using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactBookAPI.Controllers.Models;
using ContactBookAPI.Core.Interface.ContactBook;
using ContactBookAPI.Resources;
using ContactBookAPI.Wrapers;
using ContactBookAPI.Core.Interface.Services;

namespace ContactBookAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactBookController : ControllerBase
    {
        private readonly IContactBookService _contactBookService;
        private readonly IContactService _contactService;
        private readonly ILogger<ContactBookController> _logger;
        private readonly IMapper _mapper;

        public ContactBookController(IContactBookService contactBookService, IContactService contactService, ILogger<ContactBookController> logger, IMapper mapper)
        {
            _contactBookService = contactBookService;
            _contactService = contactService;
            _logger = logger;
            _mapper = mapper;
        }

        [SwaggerResponse(statusCode: 201, description: "Success creating a new contact book")]
        [SwaggerResponse(statusCode: 400, description: "Failed to create a new contact book")]
        [HttpPost]
        public async Task<IActionResult> Post(SaveContactBookResource contactBook, [FromServices] IContactBookService contactBookService)
        {
            try
            {
                if (contactBook == null)
                {
                    return BadRequest();
                }

                var response = await contactBookService.AddAsync(contactBook);
                return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error Creating Data. {ex.Message}");
            }
        }

        [SwaggerResponse(statusCode: 200, description: "Contact Book Updated successfully")]
        [SwaggerResponse(statusCode: 404, description: "Contact Book not found")]
        [HttpPut]
        public async Task<IActionResult> Update(int id, SaveContactBookResource contactBook, [FromServices] IContactBookService contactBookService)
        {
            try
            {
                if (id != contactBook.Id)
                {
                    return BadRequest("Company ID mismatch");
                }

                var contackBookToUpdate = await contactBookService.GetAsync(id);
                if (contackBookToUpdate == null)
                {
                    return NotFound($"Company with Id = {id} not found");
                }

                await contactBookService.UpdateAsync(id, contactBook.ToContactBook());
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error Updating Data. {ex.Message}");
            }
        }

        [SwaggerResponse(statusCode: 200, description: "Contact Book Deleted successfully")]
        [SwaggerResponse(statusCode: 404, description: "Contact Book not found")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromServices] IContactBookService contactBookService)
        {
            try
            {
                var contactBookToDelete = await contactBookService.GetAsync(id);
                if (contactBookToDelete is null)
                {
                    return NotFound();
                }
                await contactBookService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error Deleting Data. {ex.Message}");
            }
        }

        [SwaggerResponse(statusCode: 200, description: "Contact Books Retrieved successfully")]
        [SwaggerResponse(statusCode: 404, description: "Contact Books not found")]
        [HttpGet]
        public async Task<IActionResult> Get([FromServices] IContactBookService contactBookService)
        {
            try
            {
                var response = await contactBookService.GetAllAsync();
                if (response is null)
                {
                    return NotFound();
                }
                var resource = _mapper.Map<IEnumerable<IContactBook>, IEnumerable<ContactBookResource>>(response);
                return Ok(new Response<IEnumerable<ContactBookResource>>(resource));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error Retrieving Data. {ex.Message}");
            }
        }

        [SwaggerResponse(statusCode: 200, description: "Contact Book Retrieved successfully")]
        [SwaggerResponse(statusCode: 404, description: "Contact Book not found")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] IContactBookService contactBookService)
        {
            try
            {
                var response = await contactBookService.GetAsync(id);
                if (response is null)
                {
                    return NotFound();
                }
                var resource = _mapper.Map<IContactBook, ContactBookResource>(response);
                return Ok(new Response<ContactBookResource>(resource));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error Retrieving Data. {ex.Message}");
            }
        }
    }
}
