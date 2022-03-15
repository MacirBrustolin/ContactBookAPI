using System.ComponentModel.DataAnnotations;
using ContactBookAPI.Core.Domain.ContactBook.Company;
using ContactBookAPI.Core.Interface.ContactBook;
using ContactBookAPI.Core.Interface.ContactBook.Company;

namespace ContactBookAPI.Controllers.Models
{
    public class SaveCompanyResource
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public int ContactBookId { get; set; }

        //public ICompany ToCompany() => new Company(Id, ContactBookId, Name);

    }
}
