using ContactBookAPI.Core.Domain.Communication;
using ContactBookAPI.Core.Domain.ContactBook.Company;
using ContactBookAPI.Core.Interface.ContactBook.Company;
using ContactBookAPI.Core.Interface.Services;
using ContactBookAPI.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactBookAPI.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IContactBookRepository _contactBookRepository;

        public CompanyService(ICompanyRepository companyRepository, IContactBookRepository contactBookRepository)
        {
            _companyRepository = companyRepository;
            _contactBookRepository = contactBookRepository;
        }

        public async Task<CompanyResponse> AddAsync(Company company)
        {
            var existingContactBook = await _contactBookRepository.GetAsync(company.ContactBookId);

            if (existingContactBook == null)
                return new CompanyResponse("Contact Book not Valid.");

            try
            {
                return new CompanyResponse(await _companyRepository.SaveAsync(company));

            }
            catch (Exception ex)
            {
                return new CompanyResponse($"An error occurred when saving the company: {ex.Message}");
            } 
        }

        public async Task<CompanyResponse> FindByIdAsync(int id)
        {
            try
            {
                var existingCompany = await _companyRepository.GetAsync(id);

                if (existingCompany == null)
                    return new CompanyResponse("Company not Found.");

                return new CompanyResponse(existingCompany);
            }
            catch (Exception ex)
            {
                return new CompanyResponse($"An error occurred when retrieving the company: {ex.Message}");
            }
            
        }

        public async Task<IEnumerable<Company>> ListAsync()
        {
                return await _companyRepository.GetAllAsync();
        }

        public async Task<CompanyResponse> Remove(int id)
        {
            var existingCompany = await _companyRepository.GetAsync(id);

            if (existingCompany == null)
                return new CompanyResponse("Company not found.");

            try
            {
                await _companyRepository.DeleteAsync(id);
                return new CompanyResponse(existingCompany);
            }
            catch (Exception ex)
            {
                return new CompanyResponse($"An error occurred when deleting the company: {ex.Message}");
            }
        }

        public async Task<CompanyResponse> Update(int id, Company company)
        {
            var existingCompany = await _companyRepository.GetAsync(id);

            if (existingCompany == null)
                return new CompanyResponse("Company not found.");

            try
            {
                await _companyRepository.UpdateAsync(id, company);

                return new CompanyResponse(company);
            }
            catch (Exception ex)
            {
                return new CompanyResponse($"An error occurred when updating the company: {ex.Message}");
            }
        }
    }
}
