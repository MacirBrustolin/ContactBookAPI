using ContactBookAPI.Core.Domain.ContactBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactBookAPI.Core.Interface.Services
{
    public interface IContactBookService
    {
        Task<IEnumerable<Core.Domain.ContactBook.ContactBook>> ListAsync();
        Task<Core.Domain.ContactBook.ContactBook> FindByIdAsync(int id);
        Task AddAsync(Core.Domain.ContactBook.ContactBook contactBook);
        void Update(Core.Domain.ContactBook.ContactBook contactBook);
        void Remove(Core.Domain.ContactBook.ContactBook contactBook);
    }
}
