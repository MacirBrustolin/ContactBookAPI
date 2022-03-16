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
        Task<Contact> SaveAsync(Contact contact);
        Task UpdateAsync(int id, Contact contact);
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact> GetByIdAsync(int id);
        Task<IEnumerable<Contact>> GetAsync(int pageRows, int pageNumber, string searchString);
        Task<List<ContactCsv>> GetDataFromCSVFile(IFormFile file);
        Task DeleteAsync(int id);
        Task<int> RegistersCount(string searchString);
        Task<IEnumerable<Contact>> GetByCompanyAndContactBook(int companyId, int contactBookId);
        Task<List<int>> ContactIdList();
        //Task UploadFile(List<ContactCsv> records, List<int> companyList, List<int> contactIdList);
    }
}
