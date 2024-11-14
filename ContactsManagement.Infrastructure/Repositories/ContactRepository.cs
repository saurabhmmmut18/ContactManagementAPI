using ContactsManagement.Core.Entities;
using ContactsManagement.Core.Interfaces;
using Newtonsoft.Json;

public class ContactRepository : IContactRepository
{
    private readonly string _jsonFilePath = "contacts.json";

    public async Task<IEnumerable<Contact>> GetAllAsync()
    {
        if (!File.Exists(_jsonFilePath)) return new List<Contact>();

        var jsonData = await File.ReadAllTextAsync(_jsonFilePath);
        return JsonConvert.DeserializeObject<List<Contact>>(jsonData) ?? new List<Contact>();
    }

    public async Task AddAsync(Contact contact)
    {
        var contacts = (await GetAllAsync()).ToList();
        contact.Id = contacts.Count > 0 ? contacts.Max(c => c.Id) + 1 : 1;
        contacts.Add(contact);
        await SaveToFileAsync(contacts);
    }

    public async Task UpdateAsync(Contact contact)
    {
        var contacts = (await GetAllAsync()).ToList();
        var existingContact = contacts.FirstOrDefault(c => c.Id == contact.Id);
        if (existingContact != null)
        {
            existingContact.FirstName = contact.FirstName;
            existingContact.LastName = contact.LastName;
            existingContact.Email = contact.Email;
            await SaveToFileAsync(contacts);
        }
    }

    public async Task DeleteAsync(int id)
    {
        var contacts = (await GetAllAsync()).ToList();
        var contact = contacts.FirstOrDefault(c => c.Id == id);
        if (contact != null)
        {
            contacts.Remove(contact);
            await SaveToFileAsync(contacts);
        }
    }

    private async Task SaveToFileAsync(List<Contact> contacts)
    {
        var jsonData = JsonConvert.SerializeObject(contacts);
        await File.WriteAllTextAsync(_jsonFilePath, jsonData);
    }

    public async Task<Contact> GetByIdAsync(int id)
    {
        var contacts = await GetAllAsync();
        return contacts.FirstOrDefault(c => c.Id == id);
    }

    // Pagination support
    public async Task<IEnumerable<Contact>> GetPaginatedAsync(int page, int pageSize)
    {
        var contacts = await GetAllAsync();
        return contacts
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    // Get total count of contacts for pagination purposes
    public async Task<int> GetTotalCountAsync()
    {
        var contacts = await GetAllAsync();
        return contacts.Count();
    }
}
