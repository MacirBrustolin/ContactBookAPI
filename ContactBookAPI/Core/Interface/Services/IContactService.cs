using ContactBookAPI.Core.Domain.Communication;
using ContactBookAPI.Core.Domain.ContactBook.Contact;
using ContactBookAPI.Core.Interface.ContactBook.Contact;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactBookAPI.Core.Interface.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> ListAsync();
        Task<IEnumerable<Contact>> FindByContactBookAndCompanyAsync(int companyId, int contactBookId);
        Task<IEnumerable<Contact>> FindBySearchStringAsync(int pageRows, int pageNumber, string searchString);
        //Task AddAsync(Contact contact);
        Task<IEnumerable<Contact>> UploadAsync(IFormFile file);
        Task<ContactResponse> Remove(int id);
        Task<int> ContactRegistersCount(string searchString);
    }
}
