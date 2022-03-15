using ContactBookAPI.Core.Domain.ContactBook.Contact;
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
        void UploadAsync(IFormFile file);
        //void Remove(Contact contact);
    }
}
