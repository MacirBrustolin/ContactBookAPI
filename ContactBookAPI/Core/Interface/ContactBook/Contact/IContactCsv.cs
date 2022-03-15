using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactBookAPI.Core.Interface.ContactBook.Company;

namespace ContactBookAPI.Core.Interface.ContactBook.Contact
{
    public interface IContactCsv
    {
        int Id { get; }
        int ContactBookId { get; }
        IContactBook ContactBook { get; }
        int CompanyId { get; }
        ICompany Company { get; }
        string Name { get; }
        string Phone { get; }
        string Email { get; }
        string Address { get; }
    }
}
