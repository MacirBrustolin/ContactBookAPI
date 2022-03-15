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

        [SwaggerResponse(statusCode: 200, description: "Contacts Retrieved successfully")]
        [SwaggerResponse(statusCode: 404, description: "Contacts not found")]
        [HttpGet]
        public async Task<IActionResult> Get([FromServices] IContactService contactService)
        {
            try
            {
                var response = await contactService.GetAllAsync();
                if (response is null)
                {
                    return NotFound();
                }
                return Ok(new Response<IEnumerable<IContact>>(response));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Retrieving Data.");
            }
        }

        [SwaggerResponse(statusCode: 200, description: "Contacts Retrieved successfully")]
        [SwaggerResponse(statusCode: 404, description: "Contacts not found")]
        [HttpGet("{companyId}, {contactBookId}")]
        public async Task<IActionResult> Get(int companyId, int contactBookId, [FromServices] IContactService contactService)
        {
            try
            {
                var response = await contactService.GetByCompanyAndContactBook(companyId, contactBookId);
                if (response is null)
                {
                    return NotFound();
                }
                return Ok(new Response<IEnumerable<IContact>>(response));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Retrieving Data.");
            }

        }

        [SwaggerResponse(statusCode: 200, description: "Contacts Retrieved successfully")]
        [SwaggerResponse(statusCode: 404, description: "Contacts not found")]
        [HttpGet("{searchString}")]
        public async Task<IActionResult> Get(int pageRows, int pageNumber, string searchString, [FromServices] IContactService contactService)
        {
            try
            {
                var validFilter = new PaginationFilter(pageNumber, pageRows);
                var registersCount = contactService.RegistersCount(searchString);
                var pagedData = await contactService.GetAsync(validFilter.PageSize, validFilter.PageNumber, searchString);

                if (pagedData is null)
                {
                    return NotFound();
                }
                return Ok(new PagedResponse<IEnumerable<IContact>>(pagedData, validFilter.PageNumber, validFilter.PageSize, registersCount.Result));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Retrieving Data.");
            }
        }

        [SwaggerResponse(statusCode: 201, description: "Success creating the new contacts")]
        [SwaggerResponse(statusCode: 400, description: "Failed to create the new contacts")]
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, [FromServices] IContactService contactService, [FromServices] ICompanyService companyService)
        {
            try
            {
                var CSVData = await contactService.GetDataFromCSVFile(file);

                if (CSVData == null)
                {
                    return BadRequest();
                }
                
                var companyList = await companyService.CompanyList();
                var contactIdList = await contactService.ContactIdList();
                await contactService.UploadFile(CSVData, companyList, contactIdList);

                return Ok();
        }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Uploading Data.");
    }

}
    }
}
