using DocumentFormat.OpenXml.InkML;
using FoodsharingWebAPI.Data;
using FoodsharingWebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Npgsql;
using System.Data;

namespace FoodsharingWebAPI.Repository
{
    public class TableRepository : ITableRepository
    {
        private readonly DbContext context;

        public TableRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<DataTable> GetAllDataFromTableAsync(string tableName,CancellationToken cancellationToken)
        {
            
            var dataTable = new DataTable(tableName);

            var connection = context.Database.GetDbConnection();
            await connection.OpenAsync(cancellationToken);

            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM \"{tableName}\""; 
                using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                {
                    dataTable.Load(reader); 
                }
            }

            await connection.CloseAsync();
            return dataTable;
        }
    }
}
