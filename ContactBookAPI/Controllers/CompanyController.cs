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

        /// <summary>
        /// Saves a new company.
        /// </summary>
        /// <param name="resource">Company data.</param>
        /// <returns>Response for the request.</returns>
        [ProducesResponseType(typeof(CompanyResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaveCompanyResource resource)
        {
            var company = _mapper.Map<SaveCompanyResource, ICompany>(resource);
            var result = await _companyService.AddAsync(company);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var companyResource = _mapper.Map<ICompany, CompanyResource>(result.Resource);
            return Ok(companyResource);
        }

        /// <summary>
        /// Updates an existing company according to an identifier.
        /// </summary>
        /// <param name="id">Company identifier.</param>
        /// <param name="resource">Updated company data.</param>
        /// <returns>Response for the request.</returns>
        [ProducesResponseType(typeof(CompanyResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] SaveCompanyResource resource)
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

        /// <summary>
        /// Deletes a given company according to an identifier.
        /// </summary>
        /// <param name="id">Company identifier.</param>
        /// <returns>Response for the request.</returns>
        [ProducesResponseType(typeof(CompanyResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _companyService.Remove(id);

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
        [ProducesResponseType(typeof(IEnumerable<CompanyResource>), 200)]
        [ProducesResponseType(typeof(ErrorResource), 404)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _companyService.ListAsync();

            if (response is null)
            {
                return NotFound(new ErrorResource("List of Companies not found."));
            }

            var resource = _mapper.Map<IEnumerable<ICompany>, IEnumerable<CompanyResource>>(response);
            return Ok(resource);

        }

        /// <summary>
        /// List a given company according to an identifier.
        /// </summary>
        /// <param name="id">Company identifier.</param>
        /// <returns>Response for the request.</returns>
        [ProducesResponseType(typeof(CompanyResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _companyService.FindByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var companyResource = _mapper.Map<ICompany, CompanyResource>(result.Resource);
            return Ok(companyResource);

        }
    }
}
