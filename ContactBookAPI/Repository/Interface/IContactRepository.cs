using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactBookAPI.Core.Domain.ContactBook.Contact;
using ContactBookAPI.Core.Interface.ContactBook.Contact;

namespace ContactBookAPI.Repository.Interface
{
    public interface IContactRepository
    {
        Task<IContact> SaveAsync(IContact contact);
        Task UpdateAsync(int id, IContact contact);
        Task<IEnumerable<IContact>> GetAllAsync();
        Task<IEnumerable<IContact>> GetAsync(int pageRows, int pageNumber, string searchString);
        Task<List<ContactCsv>> GetDataFromCSVFile(IFormFile file);
        Task<int> RegistersCount(string searchString);
        Task<IEnumerable<IContact>> GetByCompanyAndContactBook(int companyId, int contactBookId);
        Task<List<int>> ContactIdList();
        Task UploadFile(List<ContactCsv> records, List<int> companyList, List<int> contactIdList);
    }
}
