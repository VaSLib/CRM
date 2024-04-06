using DAL.Enum;
using Domain.Contracts.Contact;
using Domain.Result;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet]
    public async Task<ActionResult<CollectionResult<ContactDto>>> GetAllContacts()
    {
        var result = await _contactService.GetAllContactsAsync();
        if (result.Data != null)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet("lead")]
    public async Task<ActionResult<CollectionResult<ContactDto>>> GetAllLeadContacts()
    {
        var result = await _contactService.GetAllLeadContactsAsync();
        if (result.Data != null)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPost]
    public async Task<ActionResult<BaseResult<ContactDto>>> CreateContact(ContactCreateDto contactCreateDto)
    {
        var result = await _contactService.CreateContactAsync(contactCreateDto);
        if (result.Data != null)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPut]
    public async Task<ActionResult<BaseResult<ContactDto>>> UpdateContact([FromQuery] ContactUpdateDto updateContactDto)
    {
        var result = await _contactService.UpdateContactAsync(updateContactDto);
        if (result.Data != null)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPatch("{contactId}/status")]
    public async Task<ActionResult<BaseResult<ContactDto>>> ChangeContactStatus(int contactId, ContactStatus status)
    {
        var result = await _contactService.ChangeContactStatusAsync(contactId, status);
        if (result.Data != null)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
