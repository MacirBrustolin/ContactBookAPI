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

        /// <summary>
        /// Saves a new Contact Book.
        /// </summary>
        /// <param name="resource">Contact Book data.</param>
        /// <returns>Response for the request.</returns>
        [ProducesResponseType(typeof(ContactBookResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaveContactBookResource resource)
        {
            var contactBook = _mapper.Map<SaveContactBookResource, IContactBook>(resource);
            var result = await _contactBookService.AddAsync(contactBook);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var contactBookResource = _mapper.Map<IContactBook, ContactBookResource>(result.Resource);
            return Ok(contactBookResource);
        }

        /// <summary>
        /// Updates an existing contact book according to an identifier.
        /// </summary>
        /// <param name="id">Contact Book identifier.</param>
        /// <param name="resource">Updated contact Book data.</param>
        /// <returns>Response for the request.</returns>
        [ProducesResponseType(typeof(ContactBookResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        [HttpPut]
        public async Task<IActionResult> Update(int id, SaveContactBookResource resource)
        {
            var company = _mapper.Map<SaveContactBookResource, IContactBook>(resource);
            var result = await _contactBookService.Update(id, company);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var contactBookResource = _mapper.Map<IContactBook, ContactBookResource>(result.Resource);
            return Ok(contactBookResource);
        }

        /// <summary>
        /// Deletes a given contact book according to an identifier.
        /// </summary>
        /// <param name="id">Contact Book identifier.</param>
        /// <returns>Response for the request.</returns>
        [ProducesResponseType(typeof(ContactBookResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _contactBookService.Remove(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var companyResource = _mapper.Map<IContactBook, ContactBookResource>(result.Resource);
            return Ok(companyResource);
        }

        /// <summary>
        /// Lists all contact books.
        /// </summary>
        /// <returns>List of contact books.</returns>
        [ProducesResponseType(typeof(IEnumerable<ContactBookResource>), 200)]
        [ProducesResponseType(typeof(ErrorResource), 404)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _contactBookService.ListAsync();

            if (response is null)
            {
                return NotFound(new ErrorResource("List of Companies not found."));
            }

            var resource = _mapper.Map<IEnumerable<IContactBook>, IEnumerable<ContactBookResource>>(response);
            return Ok(resource);
        }

        /// <summary>
        /// List a given contact book according to an identifier.
        /// </summary>
        /// <param name="id">Contact book identifier.</param>
        /// <returns>Response for the request.</returns>
        [ProducesResponseType(typeof(ContactBookResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _contactBookService.FindByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var contactBookResource = _mapper.Map<IContactBook, ContactBookResource>(result.Resource);
            return Ok(contactBookResource);
        }
    }
}
