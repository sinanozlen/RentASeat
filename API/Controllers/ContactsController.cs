using BusinessLayer.Abstract;
using DtoLayer.ContactDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContactsController : ControllerBase
	{
		private readonly IContactService _contactService;

		public ContactsController(IContactService contactService)
		{
			_contactService = contactService;
		}
		[HttpGet]
		public IActionResult ContactList()
		{
			var contacts = _contactService.TGetListAll();
			return Ok(contacts);
		}
		[HttpGet("{id}")]
		public IActionResult GetContact(int id)
		{
			var contact = _contactService.TGetbyID(id);
			if (contact == null)
			{
				return NotFound();
			}
			return Ok(contact);
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteContact(int id)
		{
			var contact = _contactService.TGetbyID(id);
			if (contact == null)
			{
				return NotFound();
			}
			_contactService.TDelete(contact);
			return Ok("İletişim Alanı Silme İşlemi Başarı İle Gerçekleştirildi");
		}
		[HttpPost]
		public IActionResult CreateContact(CreateContactDto createContactDto)
		{
			var contact=new Contact()
			{
				Email = createContactDto.Email,
				Message = createContactDto.Message,
				SendDate = createContactDto.SendDate,
				Name = createContactDto.Name,
				Subject = createContactDto.Subject
			};
			_contactService.TAdd(contact);
			return Ok("İletişim Alanı Oluşturma İşlemi Başarı İle Gerçekleştirildi");
		}
		[HttpPut]
		public IActionResult UpdateContact(UpdateContactDto updateContactDto)
		{
			var contact = new Contact()
			{
				ContactID = updateContactDto.ContactId,
				Email = updateContactDto.Email,
				Message = updateContactDto.Message,
				SendDate = updateContactDto.SendDate,
				Name = updateContactDto.Name,
				Subject = updateContactDto.Subject
			};
			_contactService.TUpdate(contact);
			return Ok("İletişim Alanı Güncelleme İşlemi Başarı İle Gerçekleştirildi");
		}
	}
}
