using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASOCLaViga
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
        }

        public Task<List<User>> GetPeopleAsync()
        {
            return _database.Table<User>().ToListAsync();
        }

        public Task<int> SavePersonAsync(User u)
        {
            return _database.InsertAsync(u);
        }
    }
}
