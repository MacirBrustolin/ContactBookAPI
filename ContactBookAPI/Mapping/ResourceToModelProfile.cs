using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactBookAPI.Controllers.Models;
using ContactBookAPI.Core.Domain.ContactBook;
using ContactBookAPI.Core.Domain.ContactBook.Company;
using ContactBookAPI.Core.Domain.ContactBook.Contact;
using ContactBookAPI.Core.Interface.ContactBook;
using ContactBookAPI.Core.Interface.ContactBook.Company;
using ContactBookAPI.Core.Interface.ContactBook.Contact;
using ContactBookAPI.Resources;

namespace ContactBookAPI.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveCompanyResource, ICompany> ();

            CreateMap<SaveContactBookResource, IContactBook> ();

            CreateMap<SaveContactResource, IContact> ();
        }
    }
}
