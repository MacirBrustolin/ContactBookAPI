using System.Collections.Generic;
using System.Threading.Tasks;
using ContactBookAPI.Core.Domain.ContactBook.Company;
using ContactBookAPI.Core.Interface.ContactBook.Company;

namespace ContactBookAPI.Repository.Interface
{
    public interface ICompanyRepository
    {
        Task<Company> SaveAsync(Company company);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, Company company);
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company> GetAsync(int id);
        //Task<List<int>> CompanyList();
    }
}
