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
        public async Task<ActionResult<List<Address>>> GetAll()
        {
            try
            {
                var Addresss = await addressRepository.GetAll();
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
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            try
            {
                var Address = await addressRepository.GetById(id);
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
        public async Task<IActionResult> CreateAddress([FromBody] Address Address)
        {
            if (Address == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                await addressRepository.Add(Address);
                return CreatedAtAction(nameof(GetAddress), new { id = Address.Id }, Address);
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при добавлении Address в БД: {ex.Message}");
                return StatusCode(500, "Ошибка при создании адреса");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] Address AddressUpdate)
        {
            if (AddressUpdate == null)
                return BadRequest("Тело запроса пустое");
            try
            {
                var Address = await addressRepository.GetById(id);
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

                await addressRepository.Update(Address);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ошибка при обновлении Address: {ex.Message}");
                return StatusCode(500, "Ошибка при обновлении адреса");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            try
            {
                var Address = await addressRepository.GetById(id);
                if (Address == null)
                    return NotFound("Адрес с таким id не существует");
                await addressRepository.Delete(Address);
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
