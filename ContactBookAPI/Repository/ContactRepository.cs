using CsvHelper;
using CsvHelper.Configuration;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactBookAPI.Core.Domain.ContactBook.Contact;
using ContactBookAPI.Core.Interface.ContactBook.Contact;
using ContactBookAPI.Dao;
using ContactBookAPI.Database;
using ContactBookAPI.Mapping;
using ContactBookAPI.Repository.Interface;

namespace ContactBookAPI.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public ContactRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async Task<Contact> SaveAsync(Contact contact)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);
            var dao = new ContactDao(contact);

            dao.Id = await connection.InsertAsync(dao);

            return dao.Export();
        }

        public async Task UpdateAsync(int id, Contact contact)
        {
            var dao = new ContactDao(contact) { Id = id };

            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query = "SELECT * FROM Contact where Id = @id";
            var result = await connection.QuerySingleOrDefaultAsync<ContactDao>(query, new { id });

            await connection.UpdateAsync(dao);
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query = "SELECT * FROM Contact";
            var result = await connection.QueryAsync<ContactDao>(query);

            return result?.Select(item => item.Export());
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            var list = await GetAllAsync();

            return list.ToList().Where(item => item.Id == id).FirstOrDefault();
        }

        public async Task<IEnumerable<Contact>> GetAsync(int pageRows, int pageNumber, string searchString)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query = "SELECT * FROM Contact WHERE ((Id LIKE '%' || @searchString || '%') OR (CompanyId LIKE '%' || @searchString || '%') OR (ContactBookId LIKE '%' || @searchString || '%') OR (Name LIKE '%' || @searchString || '%') OR (Phone LIKE '%' || @searchString || '%') OR (Email LIKE '%' || @searchString || '%') OR (Address LIKE '%' || @searchString || '%')) LIMIT @pageRows OFFSET @page";
            var result = await connection.QueryAsync<ContactDao>(query, new { pageRows, page = pageRows * pageNumber, searchString });

            return result?.Select(item => item.Export());
        }

        public async Task<IEnumerable<Contact>> GetByCompanyAndContactBook(int companyId, int contactBookId)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query = "SELECT Contact.Id, Contact.CompanyId, Contact.ContactBookId, Company.Id as company_id, Company.Name as company_name, ContactBook.Id as contactbook_id, ContactBook.Name as contactbook_name, Contact.Name, Contact.Phone, Contact.Email, Contact.Address FROM Contact " +
                "LEFT JOIN Company ON Company.Id = Contact.CompanyId " +
                "LEFT JOIN ContactBook ON ContactBook.Id = Contact.ContactBookId " +
                "WHERE Contact.CompanyId = @companyId AND Contact.ContactBookId = @contactBookId";
            var result = await connection.QueryAsync<ContactDao>(query, new { companyId, contactBookId });

            return result?.Select(item => item.Export());
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM Contact WHERE Id = @id;");

            await connection.ExecuteAsync(sql.ToString(), new { id }, transaction);
            transaction.Commit();
            connection.Close();
        }
        public async Task<int> RegistersCount(string searchString)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);
            var query = "SELECT COUNT(*) FROM Contact WHERE ((Id LIKE '%' || @searchString || '%') OR (CompanyId LIKE '%' || @searchString || '%') OR (ContactBookId LIKE '%' || @searchString || '%') OR (Name LIKE '%' || @searchString || '%') OR (Phone LIKE '%' || @searchString || '%') OR (Email LIKE '%' || @searchString || '%') OR (Address LIKE '%' || @searchString || '%'))";

            var result = await connection.QuerySingleOrDefaultAsync<int>(query, new { searchString });

            return result;
        }

        public async Task<List<int>> ContactIdList()
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var sql = "SELECT DISTINCT Id FROM Contact";

            var contactsIds = await connection.QueryAsync<int>(sql);

            return contactsIds.ToList();
        }

        public async Task<List<ContactCsv>> GetDataFromCSVFile(IFormFile file)
        {
            using var memoryStream = new MemoryStream(new byte[file.Length]);
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            using var reader = new StreamReader(memoryStream);

            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<ContactMap>();
            var records = csv.GetRecords<ContactCsv>();

            return records.ToList();
        }   
    }
}

