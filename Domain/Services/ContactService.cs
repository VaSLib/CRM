using AutoMapper;
using DAL.Entity;
using DAL.Enum;
using DAL.Repositories.Interfaces;
using Domain.Contacts;
using Domain.Contracts.Contact;
using Domain.Contracts.User;
using Domain.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Domain.Services;

public class ContactService : IContactService
{
    private readonly IBaseRepository<Contact> _contactRepository;
    private readonly HttpContext _httpContext;
    private readonly IMapper _mapper;

    public ContactService(IBaseRepository<Contact> contactRepository, IMapper mapper, IHttpContextAccessor accessor)
    {
        _contactRepository = contactRepository;
        _mapper = mapper;
        if (accessor.HttpContext == null)
        {
            throw new ArgumentException(nameof(accessor.HttpContext));
        }
        _httpContext = accessor.HttpContext;
    }

    public async Task<CollectionResult<ContactDto>> GetAllContactsAsync()
    {
        ContactDto[] contacts;
        try
        {
            contacts = await _contactRepository.GetAll()
                .Select(c => _mapper.Map<ContactDto>(c))
                .ToArrayAsync();

        }
        catch (Exception ex)
        {
            return new CollectionResult<ContactDto>()
            {
                ErrorMessage = ex.Message,
            };
        }

        if (!contacts.Any())
        {
            return new CollectionResult<ContactDto>()
            {
                ErrorMessage = "Contact not found",
            };
        }

        return new CollectionResult<ContactDto>()
        {
            Data = contacts,
            Count = contacts.Length

        };
    }
    public async Task<CollectionResult<ContactDto>> GetAllLeadContactsAsync()
    {
        ContactDto[] contacts;
        try
        {
            contacts = await _contactRepository.GetAll()
                .Where(c => c.Status == ContactStatus.Lead)
                .Select(c => _mapper.Map<ContactDto>(c))
                .ToArrayAsync();

        }
        catch (Exception ex)
        {
            return new CollectionResult<ContactDto>()
            {
                ErrorMessage = ex.Message,
            };
        }

        if (!contacts.Any())
        {
            return new CollectionResult<ContactDto>()
            {
                ErrorMessage = "Contact not found",
            };
        }

        return new CollectionResult<ContactDto>()
        {
            Data = contacts,
            Count = contacts.Length

        };
    }

    public async Task<BaseResult<ContactDto>> CreateContactAsync(ContactCreateDto contactCreateDto)
    {
        try
        {
            var contact = _mapper.Map<Contact>(contactCreateDto);
            int? MarketerIdValue = null;

            var claim = _httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null && int.TryParse(claim.Value, out int userId))
            {
                MarketerIdValue = userId;
            }

            contact.MarketerId = (int)MarketerIdValue;
            contact.Update = DateTime.Now;

            await _contactRepository.CreateAsync(contact);

            return new BaseResult<ContactDto>()
            {
                Data = _mapper.Map<ContactDto>(contact)
            };
        }
        catch (Exception ex)
        {
            return new BaseResult<ContactDto>()
            {
                ErrorMessage = ex.Message,
            };
        }
    }

    public async Task<BaseResult<ContactDto>> UpdateContactAsync(ContactUpdateDto updateContactDto)
    {
        try
        {
            var contact = _contactRepository.GetAll().FirstOrDefault(c => c.Id == updateContactDto.Id);
            if (contact == null)
            {
                return new BaseResult<ContactDto>()
                {
                    ErrorMessage = "Contact not found",
                };
            }

            if (updateContactDto.Name != null)
                contact.Name = updateContactDto.Name;

            if (updateContactDto.Surname != null)
                contact.Surname = updateContactDto.Surname;

            if (updateContactDto.MiddleName != null)
                contact.MiddleName = updateContactDto.MiddleName;

            if (updateContactDto.Email != null)
                contact.Email = updateContactDto.Email;

            if (updateContactDto.Phone != null)
                contact.Phone = updateContactDto.Phone;

            contact.Update = DateTime.Now;

            await _contactRepository.UpdateAsync(contact);

            return new BaseResult<ContactDto>()
            {
                Data = _mapper.Map<ContactDto>(contact)
            };

        }
        catch (Exception ex)
        {
            return new BaseResult<ContactDto>()
            {
                ErrorMessage = ex.Message,
            };
        }
    }

    public async Task<BaseResult<ContactDto>> ChangeContactStatusAsync(int contactId, ContactStatus status)
    {
        try
        {
            var contact = _contactRepository.GetAll().FirstOrDefault(c => c.Id == contactId);
            if (contact == null)
            {
                return new BaseResult<ContactDto>()
                {
                    ErrorMessage = "User not found",
                };
            }
            if (status == ContactStatus.Lead)
            {
                return new BaseResult<ContactDto>()
                {
                    ErrorMessage = "You cannot change the status to a lead. You need to create a lead",
                };
            }

            

            contact.Status = status;
            await _contactRepository.UpdateAsync(contact);
            return new BaseResult<ContactDto>()
            {
                Data = _mapper.Map<ContactDto>(contact)
            };

        }
        catch (Exception ex)
        {
            return new BaseResult<ContactDto>()
            {
                ErrorMessage = ex.Message,
            };
        }
    }


}
