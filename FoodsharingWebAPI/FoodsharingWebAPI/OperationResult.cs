namespace FoodsharingWebAPI
{
    /// <summary>
    /// Класс для передачи результатов выполнения методов
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// Успешность выполнения
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Сообщение о результате выполнения 
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// Данные, которые нужно передать в результате выполнения
        /// </summary>
        public string? Data { get; set; }

        /// <summary>
        /// Метод для формирования успешного результата выполнения
        /// </summary>
        public static OperationResult SuccessResult(string message, string data = "")
        {
            return new OperationResult { Success = true, Message = message, Data = data };
        }

        /// <summary>
        /// Метод для формирования неуспешного результата выполнения
        /// </summary>
        public static OperationResult FailureResult(string message)
        {
            return new OperationResult { Success = false, Message = message };
        }
    }
}
