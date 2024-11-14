using ContactsManagement.Core.Entities;
using ContactsManagement.Core.Interfaces;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;

    public ContactService(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<IEnumerable<Contact>> GetAllAsync() => await _contactRepository.GetAllAsync();

    public async Task<Contact> GetByIdAsync(int id) => await _contactRepository.GetByIdAsync(id);

    public async Task AddAsync(Contact contact) => await _contactRepository.AddAsync(contact);

    public async Task UpdateAsync(Contact contact) => await _contactRepository.UpdateAsync(contact);

    public async Task DeleteAsync(int id) => await _contactRepository.DeleteAsync(id);

    // Pagination
    public async Task<IEnumerable<Contact>> GetPaginatedAsync(int page, int pageSize)
    {
        return await _contactRepository.GetPaginatedAsync(page, pageSize);
    }

    // Get the total count of contacts for pagination
    public async Task<int> GetTotalCountAsync()
    {
        return await _contactRepository.GetTotalCountAsync();
    }
}
