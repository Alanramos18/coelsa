using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Coelsa.Data.Entities;
using Coelsa.Dto;

namespace Coelsa.Data
{
    public interface IContactsRepository
    {
        Task CreateAsync(ContactsDto contactsDto, CancellationToken cancellationToken);
        Task UpdateAsync(int id, ContactsDto contactsDto, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Contacts>> GetAllAsync(CancellationToken cancellationToken);
    }
}
