using System;
using System.Data.Common;
using Blog.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;



namespace BlogUnitTest
{
    public class SqliteInMemoryItemsControllerTest : BlogControllerTest, IDisposable
    {
            private readonly DbConnection _connection;

            public SqliteInMemoryItemsControllerTest()
                : base(
                    new DbContextOptionsBuilder<BlogDbContext>()
                        .UseSqlite(CreateInMemoryDatabase())
                        .Options)
            {
                _connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
            }

            private static DbConnection CreateInMemoryDatabase()
            {
                var connection = new SqliteConnection("Filename=:memory:");

                connection.Open();

                return connection;
            }

            public void Dispose() => _connection.Dispose();
        
    }
}
