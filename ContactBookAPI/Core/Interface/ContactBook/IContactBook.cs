using System;
using System.Collections.Generic;
using ContactBookAPI.Core.Interface.ContactBook.Contact;

namespace ContactBookAPI.Core.Interface.ContactBook
{
    public interface IContactBook
    {
        int Id { get; }
        string Name { get; }
        IList<IContact> Contacts { get; }
    }
}
