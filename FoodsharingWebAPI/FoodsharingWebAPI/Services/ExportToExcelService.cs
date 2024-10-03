using ClosedXML;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using FoodsharingWebAPI.Data;
using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using FoodsharingWebAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace FoodsharingWebAPI.Services
{
    /// <summary>
    /// Сервис для экспорта сущностей из БД в Excel
    /// </summary>
    public class ExportToExcelService
    {
        private readonly ITableRepository tableRepository;
        private readonly AppDbContext context;

        public ExportToExcelService(ITableRepository tableRepository, AppDbContext context)
        {
            this.tableRepository = tableRepository;
            this.context = context;
        }

        /// <summary>
        /// Экспортирует данные всех DbSet из контекста в файл Excel и возвращает файл в виде массива байтов
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Массив байтов, представляющий файл Excel</returns>
        public async Task<byte[]> ExportEntitiesToExcelAsync(CancellationToken cancellationToken = default)
        {
            using (var workbook = new XLWorkbook())
            {
                // получаем имена всех DbSetов
                  var tableNames = context.GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(prop => prop.PropertyType.IsGenericType &&
                                   prop.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                    .Select(prop => prop.Name)
                    .ToList();

                // извлекаем все данные из каждой таблицы и заносим их на новый лист экселя
                foreach (var name in tableNames)
                {
                    var tableData = await tableRepository.GetAllDataFromTableAsync(name, cancellationToken);
                    workbook.Worksheets.Add(tableData, tableData.TableName);
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}