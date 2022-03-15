using System;

namespace ContactBookAPI.Core.Interface.ContactBook.Company
{
    public interface ICompany
    {
        int Id { get; }
        string Name { get; }
        int ContactBookId { get; }
        IContactBook ContactBook { get; }
    }
}
