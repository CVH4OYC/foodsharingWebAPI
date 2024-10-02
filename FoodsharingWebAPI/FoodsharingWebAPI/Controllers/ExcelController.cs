using FoodsharingWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    /// <summary>
    /// Контроллер для экспорта данных всех сущностей БД в Excel
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly ExportToExcelService exportToExcelService;

        public ExcelController(ExportToExcelService exportToExcelService)
        {
            this.exportToExcelService = exportToExcelService;
        }

        /// <summary>
        /// Экспортирует все таблицы БД в Excel и возвращает его пользователю для скачивания
        /// </summary>
        /// <param name="cancellationToken">Токен для отмены операции</param>
        /// <returns>Файл Excel</returns>
        [HttpGet("export")]
        public async Task<IActionResult> ExportAllTablesToExcelAsync(CancellationToken cancellationToken)
        {
            var fileBytes = await exportToExcelService.ExportEntitiesToExcelAsync(cancellationToken);

            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"AllEntityData{DateTime.Now}.xlsx");
        }
    }
}
