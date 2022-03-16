using ContactBookAPI.Core.Domain.Communication;
using ContactBookAPI.Core.Domain.ContactBook;
using ContactBookAPI.Core.Interface.ContactBook;
using ContactBookAPI.Core.Interface.Services;
using ContactBookAPI.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactBookAPI.Services
{
    public class ContactBookService : IContactBookService
    {
        private readonly IContactBookRepository _contactBookRepository;

        public ContactBookService(IContactBookRepository contactBookRepository)
        {
            _contactBookRepository = contactBookRepository;
        }
        public async Task<ContactBookResponse> AddAsync(ContactBook contactBook)
        {
            try
            {
                return new ContactBookResponse(await _contactBookRepository.SaveAsync(contactBook));

            }
            catch (Exception ex)
            {
                return new ContactBookResponse($"An error occurred when saving the contact book: {ex.Message}");
            }
        }

        public async Task<ContactBookResponse> FindByIdAsync(int id)
        {
            try
            {
                var existingContactBook = await _contactBookRepository.GetAsync(id);

                if (existingContactBook == null)
                    return new ContactBookResponse("Contact Book not Found.");

                return new ContactBookResponse(existingContactBook);
            }
            catch (Exception ex)
            {
                return new ContactBookResponse($"An error occurred when retrieving the contact book: {ex.Message}");
            }
        }

        public async Task<IEnumerable<ContactBook>> ListAsync()
        {
            return await _contactBookRepository.GetAllAsync();
        }

        public async Task<ContactBookResponse> Remove(int id)
        {
            var existingContactBook = await _contactBookRepository.GetAsync(id);

            if (existingContactBook == null)
                return new ContactBookResponse("Contact book not found.");

            try
            {
                await _contactBookRepository.DeleteAsync(id);
                return new ContactBookResponse(existingContactBook);
            }
            catch (Exception ex)
            {
                return new ContactBookResponse($"An error occurred when deleting the company book: {ex.Message}");
            }
        }

        public async Task<ContactBookResponse> Update(int id, ContactBook contactBook)
        {
            var existingContactBook = await _contactBookRepository.GetAsync(id);

            if (existingContactBook == null)
                return new ContactBookResponse("Contact book not found.");

            try
            {
                await _contactBookRepository.UpdateAsync(id, contactBook);

                return new ContactBookResponse(contactBook);
            }
            catch (Exception ex)
            {
                return new ContactBookResponse($"An error occurred when updating the company book: {ex.Message}");
            }
        }
    }
}
