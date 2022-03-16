using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ContactBookAPI.Core.Domain.ContactBook;
using ContactBookAPI.Core.Domain.ContactBook.Contact;
using ContactBookAPI.Core.Interface.ContactBook;
using ContactBookAPI.Core.Interface.ContactBook.Contact;
using Dapper.Contrib.Extensions;

namespace ContactBookAPI.Dao
{
    [System.ComponentModel.DataAnnotations.Schema.Table("ContactBook")]
    public class ContactBookDao : IContactBook
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Write(false)]
        public IList<IContact> Contacts { get; set; }

        public ContactBookDao()
        {
        }

        public ContactBookDao(ContactBook contactBook)
        {
            Id = contactBook.Id;
            Name = contactBook.Name;
            Contacts = contactBook.Contacts;
        }

        public ContactBook Export() => new ContactBook(Id, Name, Contacts);
    }
}
