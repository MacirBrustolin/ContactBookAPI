using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ContactBookAPI.Controllers.Models;
using ContactBookAPI.Core.Domain.ContactBook.Contact;
using ContactBookAPI.Core.Interface.ContactBook.Contact;
using ContactBookAPI.Filters;
using ContactBookAPI.Wrapers;
using ContactBookAPI.Repository.Interface;
using CsvHelper.Configuration;
using ContactBookAPI.Mapping;
using System.Linq;
using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using ContactBookAPI.Core.Interface.Services;
using ContactBookAPI.Resources;

namespace ContactBookAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IContactBookService _contactBookService;
        private readonly IContactService _contactService;
        private readonly ILogger<ContactController> _logger;
        private readonly IMapper _mapper;

        public ContactController(ICompanyService companyService, IContactBookService contactBookService, IContactService contactService, ILogger<ContactController> logger, IMapper mapper)
        {
            _companyService = companyService;
            _contactBookService = contactBookService;
            _contactService = contactService;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Lists all contacts.
        /// </summary>
        /// <returns>List of contacts.</returns>
        [ProducesResponseType(typeof(IEnumerable<ContactResource>), 200)]
        [ProducesResponseType(typeof(ErrorResource), 404)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _contactService.ListAsync();

            if (response is null)
            {
                return NotFound(new ErrorResource("List of Companies not found."));
            }

            var resource = _mapper.Map<IEnumerable<IContact>, IEnumerable<ContactResource>>(response);
            return Ok(resource);
        }

        /// <summary>
        /// Lists all contacts by the company and contactbook identifier.
        /// </summary>
        /// <returns>List of contacts.</returns>
        [ProducesResponseType(typeof(IEnumerable<ContactResource>), 200)]
        [ProducesResponseType(typeof(ErrorResource), 404)]
        [HttpGet("{companyId}, {contactBookId}")]
        public async Task<IActionResult> Get(int companyId, int contactBookId)
        {
            var response = await _contactService.FindByContactBookAndCompanyAsync(companyId, contactBookId);

            if (response is null)
            {
                return NotFound(new ErrorResource("List of Contacts is Null."));
            }

            var resource = _mapper.Map<IEnumerable<IContact>, IEnumerable<ContactResource>>(response);
            return Ok(resource);
        }

        /// <summary>
        /// Lists all contacts by the search string.
        /// </summary>
        /// <returns>List of contacts.</returns>
        [ProducesResponseType(typeof(IEnumerable<ContactResource>), 200)]
        [ProducesResponseType(typeof(ErrorResource), 404)]
        [HttpGet("{searchString}")]
        public async Task<IActionResult> Get(int pageRows, int pageNumber, string searchString)
        {
            var validFilter = new PaginationFilter(pageNumber, pageRows);
            var registersCount = _contactService.ContactRegistersCount(searchString);
            var response = await _contactService.FindBySearchStringAsync(validFilter.PageSize, validFilter.PageNumber, searchString);

            if (response is null)
            {
                return NotFound(new ErrorResource("List of Contacts is Null."));
            }

            var resource = _mapper.Map<IEnumerable<IContact>, IEnumerable<ContactResource>>(response);
            return Ok(new PagedResponse<IEnumerable<ContactResource>>(resource, validFilter.PageNumber, validFilter.PageSize, registersCount.Result));
        }

        /// <summary>
        /// Saves the new contacts.
        /// </summary>
        /// <param name="file">CSV Contact file.</param>
        /// <returns>Response for the request.</returns>
        [ProducesResponseType(typeof(CompanyResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var response = await _contactService.UploadAsync(file);

            if (response is null)
            {
                return BadRequest(new ErrorResource("Problem when uploading the list of contacts."));
            }

            var resource = _mapper.Map<IEnumerable<IContact>, IEnumerable<ContactResource>>(response);
            return Ok(resource);
        }
    }
}
