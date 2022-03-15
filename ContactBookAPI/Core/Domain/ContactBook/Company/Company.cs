using ContactBookAPI.Core.Interface.ContactBook;
using ContactBookAPI.Core.Interface.ContactBook.Company;

namespace ContactBookAPI.Core.Domain.ContactBook.Company
{
    public class Company : ICompany
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int ContactBookId { get; private set; }
        public IContactBook ContactBook { get; private set; }

        public Company()
        {

        }

        public Company(int id, int contactBookId, IContactBook contactBook, string name)
        {
            Id = id;
            ContactBookId = contactBookId;
            ContactBook = contactBook;
            Name = name;
        }
    }
}
