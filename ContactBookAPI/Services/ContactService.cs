using ContactBookAPI.Core.Domain.Communication;
using ContactBookAPI.Core.Domain.ContactBook.Contact;
using ContactBookAPI.Core.Interface.ContactBook.Contact;
using ContactBookAPI.Core.Interface.Services;
using ContactBookAPI.Repository.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactBookAPI.Service
{
    public class ContactService : IContactService

    {
        private readonly IContactRepository _contactRepository;
        private readonly ICompanyRepository _companyRepository;

        public ContactService(IContactRepository contactRepository, ICompanyRepository companyRepository)
        {
            _contactRepository = contactRepository;
            _companyRepository = companyRepository;
        }

        public async Task<int> ContactRegistersCount(string searchString)
        {
            return await _contactRepository.RegistersCount(searchString);
        }

        public async Task<IEnumerable<Contact>> FindByContactBookAndCompanyAsync(int companyId, int contactBookId)
        {
            return await _contactRepository.GetByCompanyAndContactBook(companyId, contactBookId);
        }

        public async Task<IEnumerable<Contact>> FindBySearchStringAsync(int pageRows, int pageNumber, string searchString)
        {
            return await _contactRepository.GetAsync(pageRows, pageNumber, searchString);
        }

        public async Task<IEnumerable<Contact>> ListAsync()
        {
            return await _contactRepository.GetAllAsync();
        }

        public async Task<ContactResponse> Remove(int id)
        {
            var existingContact = await _contactRepository.GetByIdAsync(id);

            if (existingContact == null)
                return new ContactResponse("Contact not found.");

            try
            {
                await _contactRepository.DeleteAsync(id);
                return new ContactResponse(existingContact);
            }
            catch (Exception ex)
            {
                return new ContactResponse($"An error occurred when deleting the contact: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Contact>> UploadAsync(IFormFile file)
        {
            var CSVData = await _contactRepository.GetDataFromCSVFile(file);
            var companyList = await _companyRepository.GetAllAsync();
            var contactIdList = await _contactRepository.ContactIdList();

            var contatos = new List<Contact>();
            int companyIdAux = 0;

            foreach (var record in CSVData)
            {
                foreach (var company in companyList.Select(p => p.Id).Distinct())
                {
                    if (Convert.ToInt32(company) == record.CompanyId)
                    {
                        companyIdAux = record.CompanyId;
                        break;
                    }
                    else
                    {
                        companyIdAux = 0;
                    }
                }
                contatos.Add(new Contact(record.Id,
                                         record.ContactBookId,
                                         null,
                                         companyIdAux,
                                         null,
                                         record.Name,
                                         record.Phone,
                                         record.Email,
                                         record.Address));
            }

            foreach (var contato in contatos)
            {
                if (contactIdList.Contains(contato.Id))
                {
                    if (contato.ContactBookId > 0)
                    {
                        await _contactRepository.UpdateAsync(contato.Id, contato);
                    }
                }
                else
                {
                    if (contato.ContactBookId > 0)
                    {
                        await _contactRepository.SaveAsync(contato);
                    }

                }
            }
            return await _contactRepository.GetAllAsync();
        }
    }
}
