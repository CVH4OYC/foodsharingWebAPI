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
                var Addresss = await addressRepository.GetAllAsync();
                if (Addresss != null)
                    return Ok(Addresss);
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
                var Address = await addressRepository.GetByIdAsync(id);
                if (Address != null)
                    return Ok(Address);
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
        public async Task<IActionResult> CreateAddressAsync([FromBody] Address Address)
        {
            if (Address == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await addressRepository.AddAsync(Address);
                return CreatedAtAction(nameof(GetAddressAsync), new { id = Address.Id }, Address);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Address в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании адреса");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddressAsync(int id, [FromBody] Address AddressUpdate)
        {
            if (AddressUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                var Address = await addressRepository.GetByIdAsync(id);
                if (Address == null)
                    return NotFound("Адрес с таким id не существует");
                
                if(!string.IsNullOrEmpty(AddressUpdate.Region))
                    Address.Region = AddressUpdate.Region;
                if (!string.IsNullOrEmpty(AddressUpdate.City))
                    Address.City = AddressUpdate.City;
                if (!string.IsNullOrEmpty(AddressUpdate.Street))
                    Address.Street = AddressUpdate.Street;
                if (!string.IsNullOrEmpty(AddressUpdate.House))
                    Address.House = AddressUpdate.House;

                await addressRepository.UpdateAsync(Address);
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
                var Address = await addressRepository.GetByIdAsync(id);
                if (Address == null)
                    return NotFound("Адрес с таким id не существует");
                await addressRepository.DeleteAsync(Address);
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
