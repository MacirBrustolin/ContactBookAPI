using ContactBookAPI.Core.Domain.Communication;
using ContactBookAPI.Core.Domain.ContactBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactBookAPI.Core.Interface.Services
{
    public interface IContactBookService
    {
        Task<IEnumerable<Domain.ContactBook.ContactBook>> ListAsync();
        Task<ContactBookResponse> FindByIdAsync(int id);
        Task<ContactBookResponse> AddAsync(Domain.ContactBook.ContactBook contactBook);
        Task<ContactBookResponse> Update(int id, Domain.ContactBook.ContactBook contactBook);
        Task<ContactBookResponse> Remove(int id);
    }
}
