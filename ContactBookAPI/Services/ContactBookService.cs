using ContactBookAPI.Core.Domain.ContactBook;
using ContactBookAPI.Core.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactBookAPI.Services
{
    public class ContactBookService : IContactBookService
    {
        public Task AddAsync(ContactBook contactBook)
        {
            throw new NotImplementedException();
        }

        public Task<ContactBook> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ContactBook>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public void Remove(ContactBook contactBook)
        {
            throw new NotImplementedException();
        }

        public void Update(ContactBook contactBook)
        {
            throw new NotImplementedException();
        }
    }
}
