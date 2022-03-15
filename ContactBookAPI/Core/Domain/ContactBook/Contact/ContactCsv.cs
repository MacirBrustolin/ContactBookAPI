using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactBookAPI.Core.Interface.ContactBook;
using ContactBookAPI.Core.Interface.ContactBook.Company;
using ContactBookAPI.Core.Interface.ContactBook.Contact;

namespace ContactBookAPI.Core.Domain.ContactBook.Contact
{
    public class ContactCsv : IContactCsv
    {
        public int Id { get; set; }
        public int ContactBookId { get; set; }
        public IContactBook ContactBook { get; set; }
        public int CompanyId { get; set; }
        public ICompany Company { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
