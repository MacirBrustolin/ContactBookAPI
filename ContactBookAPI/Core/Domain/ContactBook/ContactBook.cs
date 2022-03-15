using System;
using System.Collections.Generic;
using ContactBookAPI.Core.Interface.ContactBook;
using ContactBookAPI.Core.Interface.ContactBook.Contact;

namespace ContactBookAPI.Core.Domain.ContactBook
{
    public class ContactBook : IContactBook
    {
        public int Id { get;  set; }
        public string Name { get;  set; }
        public IList<IContact> Contacts { get; } = new List<IContact>();

        public ContactBook()
        {

        }

        public ContactBook(int id, string name, IList<IContact> contacts)
        {
            Id = id;
            Name = name;
            Contacts = contacts;
        }

        internal object ToContactBook()
        {
            throw new NotImplementedException();
        }
    }
}
