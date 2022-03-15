using ContactBookAPI.Core.Interface.ContactBook.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactBookAPI.Core.Domain.Communication
{
    public class ContactResponse : BaseResponse<IContact>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="contact">Saved category.</param>
        /// <returns>Response.</returns>
        public ContactResponse(IContact contact) : base(contact)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public ContactResponse(string message) : base(message)
        { }
    }
}
