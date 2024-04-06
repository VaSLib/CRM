using DAL.Enum;
using Domain.Contracts.Contact;
using Domain.Result;

namespace Domain.Services
{
    public interface IContactService
    {
        Task<BaseResult<ContactDto>> ChangeContactStatusAsync(int contactId, ContactStatus status);
        Task<BaseResult<ContactDto>> CreateContactAsync(ContactCreateDto contactCreateDto);
        Task<CollectionResult<ContactDto>> GetAllContactsAsync();
        Task<CollectionResult<ContactDto>> GetAllLeadContactsAsync();
        Task<BaseResult<ContactDto>> UpdateContactAsync(ContactUpdateDto updateContactDto);
    }
}