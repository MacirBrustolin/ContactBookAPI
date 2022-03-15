using System.Collections.Generic;
using System.Threading.Tasks;
using ContactBookAPI.Core.Interface.ContactBook;

namespace ContactBookAPI.Repository.Interface
{
    public interface IContactBookRepository
    {
        Task<IContactBook> SaveAsync(IContactBook contactBook);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, IContactBook contactBook);
        Task<IEnumerable<IContactBook>> GetAllAsync();
        Task<IContactBook> GetAsync(int id);
    }
}
