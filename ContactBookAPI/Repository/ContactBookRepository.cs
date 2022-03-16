using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactBookAPI.Core.Domain.ContactBook;
using ContactBookAPI.Core.Interface.ContactBook;
using ContactBookAPI.Dao;
using ContactBookAPI.Database;
using ContactBookAPI.Repository.Interface;

namespace ContactBookAPI.Repository
{
    public class ContactBookRepository : IContactBookRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public ContactBookRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }


        public async Task<ContactBook> SaveAsync(ContactBook contactBook)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);
            var dao = new ContactBookDao(contactBook);

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
            sql.AppendLine("DELETE FROM ContactBook WHERE Id = @id;");
            //sql.AppendLine("UPDATE Contact SET ContactBookId = 0 WHERE ContactBookId = @id;");
            //sql.AppendLine("UPDATE Company SET ContactBookId = 0 WHERE ContactBookId = @id;");

            await connection.ExecuteAsync(sql.ToString(), new{ id }, transaction);
            transaction.Commit();
            connection.Close();
        }

        public async Task UpdateAsync(int id, ContactBook contactBook)
        {
            var dao = new ContactBookDao(contactBook) { Id = id };

            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            await connection.UpdateAsync(dao);
        }


        public async Task<IEnumerable<ContactBook>> GetAllAsync()
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query = "SELECT * FROM ContactBook";
            var result = await connection.QueryAsync<ContactBookDao>(query);

            return result?.Select(item => item.Export());
        }

        public async Task<ContactBook> GetAsync(int id)
        {
            var list = await GetAllAsync();

            return list.ToList().Where(item => item.Id == id).FirstOrDefault();
        }
    }
}
