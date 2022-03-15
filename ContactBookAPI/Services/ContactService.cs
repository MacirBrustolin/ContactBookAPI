using ContactBookAPI.Core.Domain.ContactBook.Contact;
using ContactBookAPI.Core.Interface.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactBookAPI.Service
{
    public class ContactService : IContactService
    {
        public Task<IEnumerable<Contact>> FindByContactBookAndCompanyAsync(int companyId, int contactBookId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Contact>> FindBySearchStringAsync(int pageRows, int pageNumber, string searchString)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Contact>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public void UploadAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
