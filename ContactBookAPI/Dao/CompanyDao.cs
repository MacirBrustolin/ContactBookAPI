using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ContactBookAPI.Core.Domain.ContactBook.Company;
using ContactBookAPI.Core.Domain.ContactBook.Contact;
using ContactBookAPI.Core.Interface.ContactBook;
using ContactBookAPI.Core.Interface.ContactBook.Company;

namespace ContactBookAPI.Dao
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Company")]
    public class CompanyDao : ICompany
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        [Required]
        public int ContactBookId { get; set; }
        [Write(false)]
        public IContactBook ContactBook { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public CompanyDao()
        {
        }

        public CompanyDao(Company company)
        {
            Id = company.Id;
            ContactBookId = company.ContactBookId;
            ContactBook = company.ContactBook;
            Name = company.Name;
        }

        public Company Export() => new Company(Id, ContactBookId, ContactBook, Name);
    }
}
