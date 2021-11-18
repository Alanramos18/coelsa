using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Coelsa.Business.Services.Interfaces;
using Coelsa.Data;
using Coelsa.Data.Entities;
using Coelsa.Dto;

namespace Coelsa.Business.Services
{
    public class ContactsServices : IContactsServices
    {
        private readonly IContactsRepository _contactsRepository;

        public ContactsServices(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
        }

        /// <inheritdoc />
        public Task CreateAsync(ContactsDto contactsDto, CancellationToken cancellationToken)
        {
            return _contactsRepository.CreateAsync(contactsDto, cancellationToken);
        }

        /// <inheritdoc />
        public Task UpdateAsync(int id, ContactsDto contactsDto, CancellationToken cancellationToken)
        {
            return _contactsRepository.UpdateAsync(id, contactsDto, cancellationToken);
        }

        /// <inheritdoc />
        public Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            return _contactsRepository.DeleteAsync(id, cancellationToken);
        }

        /// <inheritdoc />
        public Task<IEnumerable<Contacts>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _contactsRepository.GetAllAsync(cancellationToken);
        }
    }
}
