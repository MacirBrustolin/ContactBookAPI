using ContactBookAPI.Core.Interface.ContactBook;
using ContactBookAPI.Core.Interface.ContactBook.Company;
using ContactBookAPI.Core.Interface.ContactBook.Contact;

namespace ContactBookAPI.Core.Domain.ContactBook.Contact
{
    public class Contact : IContact
    {
        public int Id { get; private set; }
        public int ContactBookId { get; private set; }
        public IContactBook ContactBook { get; private set; }
        public int CompanyId { get; private set; }
        public ICompany Company { get; private set; }
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }

        public Contact(int id, int contactBookId, IContactBook contactBook, int companyId, ICompany company, string name, string phone, string email, string address)
        {
            Id = id;
            ContactBookId = contactBookId;
            ContactBook = contactBook;
            CompanyId = companyId;
            Company = company;
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
        }
    }
}
