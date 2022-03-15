using System.Collections.Generic;
using System.Threading.Tasks;
using ContactBookAPI.Core.Domain.ContactBook.Company;
using ContactBookAPI.Core.Interface.ContactBook.Company;

namespace ContactBookAPI.Repository.Interface
{
    public interface ICompanyRepository
    {
        Task<ICompany> SaveAsync(ICompany company);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, ICompany company);
        Task<IEnumerable<ICompany>> GetAllAsync();
        Task<ICompany> GetAsync(int id);
        //Task<List<int>> CompanyList();
    }
}
