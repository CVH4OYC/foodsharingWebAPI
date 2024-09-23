using FoodsharingWebAPI.Interfaces;
using FoodsharingWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodsharingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IRepository<Address> addressRepository;
        private readonly ILogger<AddressController> logger;
        public AddressController(IRepository<Address> addressRepository, ILogger<AddressController> logger)
        {
            this.addressRepository = addressRepository;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<Address>>> GetAllAsync()
        {
            try
            {
                var addresss = await addressRepository.GetAllAsync();
                if (addresss != null)
                    return Ok(addresss);
                else
                    return NotFound("Адресов не существует");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении списка Address: {ex.Message}");
                return StatusCode(500, "Ошибка при получении списка адресов");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddressAsync(int id)
        {
            try
            {
                var address = await addressRepository.GetByIdAsync(id);
                if (address != null)
                    return Ok(address);
                else
                    return NotFound("Адрес с заданным id не найден");
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при получении роли с id = {id}: {ex.Message}");
                return StatusCode(500, "Ошибка при получении адреса");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateAddressAsync([FromBody] Address address)
        {
            if (address == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await addressRepository.AddAsync(address);
                return CreatedAtAction(nameof(GetAddressAsync), new { id = address.Id }, address);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Address в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании адреса");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAddressAsync([FromBody] Address addressUpdate)
        {
            if (addressUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await addressRepository.UpdateAsync(addressUpdate);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при обновлении Address: {ex.Message}");
                return StatusCode(500, "Ошибка при обновлении адреса");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddressAsync(int id)
        {
            try
            {
                var address = await addressRepository.GetByIdAsync(id);
                if (address == null)
                    return NotFound("Адрес с таким id не существует");
                await addressRepository.DeleteAsync(address);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при удалении Address: {ex.Message}");
                return StatusCode(500, "Адрес не был удалён");
            }
        }
    }
}
