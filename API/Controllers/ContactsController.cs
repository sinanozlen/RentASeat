using BusinessLayer.Abstract;
using DtoLayer.ContactDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(IContactService contactService, ILogger<ContactsController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult ContactList()
        {
            try
            {
                var contacts = _contactService.TGetListAll();
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İletişim alanları getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetContact(int id)
        {
            try
            {
                var contact = _contactService.TGetbyID(id);
                if (contact == null)
                {
                    return NotFound("İletişim alanı bulunamadı");
                }
                return Ok(contact);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {id} olan iletişim alanı getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            try
            {
                var contact = _contactService.TGetbyID(id);
                if (contact == null)
                {
                    return NotFound("İletişim alanı bulunamadı");
                }

                _contactService.TDelete(contact);
                return Ok("İletişim alanı silme işlemi başarı ile gerçekleştirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {id} olan iletişim alanı silinirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpPost]
        public IActionResult CreateContact(CreateContactDto createContactDto)
        {
            try
            {
                var contact = new Contact
                {
                    Email = createContactDto.Email,
                    Message = createContactDto.Message,
                    SendDate = createContactDto.SendDate,
                    Name = createContactDto.Name,
                    Subject = createContactDto.Subject
                };
                _contactService.TAdd(contact);
                return Ok("İletişim alanı oluşturma işlemi başarı ile gerçekleştirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İletişim alanı eklenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpPut]
        public IActionResult UpdateContact(UpdateContactDto updateContactDto)
        {
            try
            {
                var contact = new Contact
                {
                    ContactID = updateContactDto.ContactId,
                    Email = updateContactDto.Email,
                    Message = updateContactDto.Message,
                    SendDate = updateContactDto.SendDate,
                    Name = updateContactDto.Name,
                    Subject = updateContactDto.Subject
                };
                _contactService.TUpdate(contact);
                return Ok("İletişim alanı güncelleme işlemi başarı ile gerçekleştirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {updateContactDto.ContactId} olan iletişim alanı güncellenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }
    }
}
