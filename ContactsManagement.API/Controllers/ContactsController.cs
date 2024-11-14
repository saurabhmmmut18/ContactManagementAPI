using ContactsManagement.Core.Entities;
using ContactsManagement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManagement.API.Controllers
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
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var contacts = await _contactService.GetPaginatedAsync(page, pageSize);
            var totalCount = await _contactService.GetTotalCountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            // Return paginated result
            return Ok(new { contacts, totalCount, totalPages });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await _contactService.GetByIdAsync(id);
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        [HttpPost]
        public async Task<ActionResult<Contact>> CreateContact(Contact contact)
        {
            await _contactService.AddAsync(contact);
            return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, Contact contact)
        {
            if (id != contact.Id) return BadRequest();
            await _contactService.UpdateAsync(contact);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            await _contactService.DeleteAsync(id);
            return NoContent();
        }
    }
}
