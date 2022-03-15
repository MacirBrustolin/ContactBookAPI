using System.ComponentModel.DataAnnotations;
using ContactBookAPI.Core.Domain.ContactBook;
using ContactBookAPI.Core.Interface.ContactBook;

namespace ContactBookAPI.Controllers.Models
{
    public class SaveContactBookResource
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        //public IContactBook ToContactBook() => new ContactBook(Id, Name);

    }
}
