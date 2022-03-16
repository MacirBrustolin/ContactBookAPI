using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactBookAPI.Core.Domain.ContactBook;
using ContactBookAPI.Core.Domain.ContactBook.Company;
using ContactBookAPI.Core.Interface.ContactBook.Company;
using ContactBookAPI.Dao;
using ContactBookAPI.Database;
using ContactBookAPI.Repository.Interface;

namespace ContactBookAPI.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IContactBookRepository _contactBookRepository;
        private readonly DatabaseConfig databaseConfig;

        public CompanyRepository( IContactBookRepository contactBookRepository, DatabaseConfig databaseConfig)
        {
            _contactBookRepository = contactBookRepository;
            this.databaseConfig = databaseConfig;
        }

        public async Task<Company> SaveAsync(Company company)
        {
            var existingContactBookId = await _contactBookRepository.GetAsync(company.ContactBookId);
            if (existingContactBookId == null)
                throw new ArgumentException("Given Contact Book Id not found.");

            using var connection = new SqliteConnection(databaseConfig.ConnectionString);
            var dao = new CompanyDao(company);

            if (dao.Id == 0)
                dao.Id = await connection.InsertAsync(dao);
            else
                await connection.UpdateAsync(dao);

            return dao.Export();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM Company WHERE Id = @id;");
            sql.AppendLine("UPDATE Contact SET CompanyId = null WHERE CompanyId = @id;");

            await connection.ExecuteAsync(sql.ToString(), new { id }, transaction);
            transaction.Commit();
            connection.Close();
        }

        public async Task UpdateAsync(int id, Company company)
        {
            var dao = new CompanyDao(company) { Id = id };

            var existingContactBookId = await _contactBookRepository.GetAsync(company.ContactBookId);
            if (existingContactBookId == null)
                throw new ArgumentException("Given Contact Book Id not found.");

            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query = "SELECT * FROM Company where Id = @id";
            var result = await connection.QuerySingleOrDefaultAsync<CompanyDao>(query, new { id });

            await connection.UpdateAsync(dao);
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query = "SELECT Company.Id, Company.Name, Company.ContactBookId, ContactBook.Id, ContactBook.Name FROM Company INNER JOIN ContactBook ON ContactBook.Id = Company.ContactBookId";
            var result = await connection.QueryAsync<CompanyDao, ContactBookDao, CompanyDao>(query, map: (company, contactBook) =>
            {
                company.ContactBook = contactBook;
                return company;
            });

            return result?.Select(item => item.Export()); ;
        }

        public async Task<Company> GetAsync(int id)
        {

            var list = await GetAllAsync();

            return list.ToList().Where(item => item.Id == id).FirstOrDefault();
        }

        //public async Task<List<int>> CompanyList()
        //{
        //    using var connection = new SqliteConnection(databaseConfig.ConnectionString);

        //    var sql = "SELECT DISTINCT Id FROM Company";

        //    var companies = await connection.QueryAsync<int>(sql);

        //    return companies.ToList();
        //}
    }
}
