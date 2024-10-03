using System.Data;

namespace FoodsharingWebAPI.Interfaces
{
    public interface ITableRepository
    {
        /// <summary>
        /// Извлекает все записи для сущности с указанным именем
        /// </summary>
        /// <param name="tableName">Имя сущности (таблицы)</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>DataTable со всеми записями для указанной сущности</returns>
        Task<DataTable> GetAllDataFromTableAsync (string tableName,CancellationToken cancellationToken);

    }
}
