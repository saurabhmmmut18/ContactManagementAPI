﻿using ContactsManagement.Core.Entities;

namespace ContactsManagement.Core.Interfaces
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact> GetByIdAsync(int id);
        Task AddAsync(Contact contact);
        Task UpdateAsync(Contact contact);
        Task DeleteAsync(int id);

        // Pagination support
        Task<IEnumerable<Contact>> GetPaginatedAsync(int page, int pageSize);

        // Get total count of contacts for pagination purposes
        Task<int> GetTotalCountAsync();
    }
}
