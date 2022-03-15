using ContactBookAPI.Core.Interface.ContactBook.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactBookAPI.Core.Domain.Communication
{
    public class CompanyResponse : BaseResponse<ICompany>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="company">Saved category.</param>
        /// <returns>Response.</returns>
        public CompanyResponse(ICompany company) : base(company)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public CompanyResponse(string message) : base(message)
        { }
    }
}
