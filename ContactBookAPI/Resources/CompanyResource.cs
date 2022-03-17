using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactBookAPI.Core.Interface.ContactBook;

namespace ContactBookAPI.Resources
{
    public class CompanyResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public ContactBookResource ContactBook { get; set; }
    }
}
