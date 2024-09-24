using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class ErrorController : ControllerBase
    {
        /// <summary>

        /// Метод контроллера для обработки маршрута /error

        /// </summary>
        [Route("/error")]
        public IActionResult HandleError() => Problem();
    }

}
