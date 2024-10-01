using System.ComponentModel.DataAnnotations;

namespace FoodsharingWebAPI.DTO
{
    /// <summary>
    /// DTO для передачи данных пользователя при регистрации и входе в систему
    /// </summary>
    public class RegLogUserRequest
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        public string UserName { get; set; }

        ///
        /// Пароль пользователя
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
