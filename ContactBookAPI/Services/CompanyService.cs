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

        public async Task<CompanyResponse> AddAsync(ICompany company)
        {
            try
            {
                return new CompanyResponse(await _companyRepository.SaveAsync(company));

            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CompanyResponse($"An error occurred when saving the category: {ex.Message}");
            }
            
        }

        public async Task<CompanyResponse> FindByIdAsync(int id)
        {
            return new CompanyResponse(await _companyRepository.GetAsync(id));
        }

        public async Task<IEnumerable<ICompany>> ListAsync()
        {
            return await _companyRepository.GetAllAsync();
        }

        public async Task<CompanyResponse> Remove(int id)
        {
            var existingCompany = await _companyRepository.GetAsync(id);

            if (existingCompany == null)
                return new CompanyResponse("Category not found.");

            try
            {
                await _companyRepository.DeleteAsync(id);
                return new CompanyResponse(existingCompany);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CompanyResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }

        public async Task<CompanyResponse> Update(int id, ICompany company)
        {
            var existingCompany = await _companyRepository.GetAsync(company.Id);

            if (existingCompany == null)
                return new CompanyResponse("Category not found.");

            try
            {
                await _companyRepository.UpdateAsync(id, company);

                return new CompanyResponse(company);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CompanyResponse($"An error occurred when updating the category: {ex.Message}");
            }
        }
    }
}
