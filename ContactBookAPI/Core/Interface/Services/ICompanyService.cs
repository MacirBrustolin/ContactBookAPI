using ContactBookAPI.Core.Domain.Communication;
using ContactBookAPI.Core.Domain.ContactBook.Company;
using ContactBookAPI.Core.Interface.ContactBook.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactBookAPI.Core.Interface.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> ListAsync();
        Task<CompanyResponse> FindByIdAsync(int id);
        Task<CompanyResponse> AddAsync(Company company);
        Task<CompanyResponse> Update(int id, Company company);
        Task<CompanyResponse> Remove(int id);
    }
}
