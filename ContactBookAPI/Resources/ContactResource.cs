using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactBookAPI.Core.Domain.ContactBook;
using ContactBookAPI.Core.Domain.ContactBook.Company;
using ContactBookAPI.Core.Interface.ContactBook;
using ContactBookAPI.Core.Interface.ContactBook.Company;

namespace ContactBookAPI.Resources
{
    public class ContactResource
    {
        public int Id { get; set; }
        public ContactBookResource ContactBook { get; set; }
        public CompanyResource Company { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
