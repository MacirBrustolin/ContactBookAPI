using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactBookAPI.Controllers.Models;
using ContactBookAPI.Core.Domain.ContactBook.Company;
using ContactBookAPI.Core.Interface.ContactBook.Company;
using ContactBookAPI.Resources;
using ContactBookAPI.Wrapers;
using ContactBookAPI.Core.Interface.Services;
using ContactBookAPI.Extentions;

namespace ContactBookAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IContactBookService _contactBookService;
        private readonly ILogger<CompanyController> _logger;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IContactBookService contactBookService, ILogger<CompanyController> logger, IMapper mapper)
        {
            _companyService = companyService;
            _contactBookService = contactBookService;
            _logger = logger;
            _mapper = mapper;
        }

        [SwaggerResponse(statusCode: 201, description: "Success creating a company")]
        [SwaggerResponse(statusCode: 400, description: "Failed to create a new company")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaveCompanyResource resource, [FromServices] ICompanyService companyService)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            try
            {
                var company = _mapper.Map<SaveCompanyResource, ICompany>(resource);

                if (company == null)
                {
                    return BadRequest(new ErrorResource("Company is not defined."));
                }

                var response = await companyService.AddAsync(company);
                return CreatedAtAction(nameof(Get), new { id = response.Resource.Id }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorResource(ex.Message));
            }

        }

        [SwaggerResponse(statusCode: 200, description: "Company Updated successfully")]
        [SwaggerResponse(statusCode: 404, description: "Company not found")]
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] SaveCompanyResource resource, [FromServices] ICompanyService companyService)
        {
            var company = _mapper.Map<SaveCompanyResource, ICompany>(resource);
            var result = await _companyService.Update(id, company);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var categoryResource = _mapper.Map<ICompany, CompanyResource>(result.Resource);
            return Ok(categoryResource);

        }

        [SwaggerResponse(statusCode: 200, description: "Company Deleted successfully")]
        [SwaggerResponse(statusCode: 404, description: "Company not found")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, [FromServices] ICompanyService companyService)
        {
            var result = await companyService.Remove(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var companyResource = _mapper.Map<ICompany, CompanyResource>(result.Resource);
            return Ok(companyResource);

        }

        /// <summary>
        /// Lists all companies.
        /// </summary>
        /// <returns>List os companies.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CompanyResource>), 200)]
        public async Task<IActionResult> Get([FromServices] ICompanyService companyService)
        {
            var response = await companyService.ListAsync();

            if (response is null)
            {
                return NotFound(new ErrorResource("List of Companies not found."));
            }

            var resource = _mapper.Map<IEnumerable<ICompany>, IEnumerable<CompanyResource>>(response);
            return Ok(resource);

        }

        [SwaggerResponse(statusCode: 200, description: "Company Retrieved successfully")]
        [SwaggerResponse(statusCode: 404, description: "Company not found")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] ICompanyService companyService)
        {
            var result = await companyService.FindByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var companyResource = _mapper.Map<ICompany, CompanyResource>(result.Resource);
            return Ok(companyResource);

        }
    }
}
