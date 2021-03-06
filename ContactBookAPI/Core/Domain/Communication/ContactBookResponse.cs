using ContactBookAPI.Core.Interface.ContactBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactBookAPI.Core.Domain.Communication
{
    public class ContactBookResponse : BaseResponse<Domain.ContactBook.ContactBook>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="contactBook">Saved category.</param>
        /// <returns>Response.</returns>
        public ContactBookResponse(Domain.ContactBook.ContactBook contactBook) : base(contactBook)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public ContactBookResponse(string message) : base(message)
        { }
    }
}
