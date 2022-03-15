using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactBookAPI.Core.Domain.ContactBook;
using ContactBookAPI.Core.Domain.ContactBook.Company;
using ContactBookAPI.Core.Domain.ContactBook.Contact;
using ContactBookAPI.Core.Interface.ContactBook;
using ContactBookAPI.Core.Interface.ContactBook.Company;
using ContactBookAPI.Core.Interface.ContactBook.Contact;
using ContactBookAPI.Resources;

namespace ContactBookAPI.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile (){

            CreateMap<ICompany, CompanyResource>();

            CreateMap<IContactBook, ContactBookResource>();

            CreateMap<IContact, ContactResource>();
        }
        
    }
}
