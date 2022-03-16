using System.Collections.Generic;
using System.Threading.Tasks;
using ContactBookAPI.Core.Domain.ContactBook;
using ContactBookAPI.Core.Interface.ContactBook;

namespace ContactBookAPI.Repository.Interface
{
    public interface IContactBookRepository
    {
        Task<ContactBook> SaveAsync(ContactBook contactBook);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, ContactBook contactBook);
        Task<IEnumerable<ContactBook>> GetAllAsync();
        Task<ContactBook> GetAsync(int id);
    }
}
