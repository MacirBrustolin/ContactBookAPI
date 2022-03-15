using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ContactBookAPI.Core.Domain.ContactBook;
using ContactBookAPI.Core.Domain.ContactBook.Company;
using ContactBookAPI.Core.Domain.ContactBook.Contact;
using ContactBookAPI.Core.Interface.ContactBook;
using ContactBookAPI.Core.Interface.ContactBook.Company;
using ContactBookAPI.Core.Interface.ContactBook.Contact;
using Dapper.Contrib.Extensions;

namespace ContactBookAPI.Dao
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Contact")]
    public class ContactDao : IContact
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        [Required]
        public int ContactBookId { get; set; }
        [Write(false)]
        public IContactBook ContactBook { get; set; }
        public int CompanyId { get; set; }
        [Write(false)]
        public ICompany Company { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(100)]
        public string Address { get; set; }

        public ContactDao()
        {
        }

        public ContactDao(IContact contact)
        {
            Id = contact.Id;
            ContactBookId = contact.ContactBookId;
            CompanyId = contact.CompanyId;
             Name = contact.Name;
            Phone = contact.Phone;
            Email = contact.Email;
            Address = contact.Address;
        }

        public IContact Export() => new Contact(Id, ContactBookId, ContactBook, CompanyId, Company, Name, Phone, Email, Address);
    }
}
