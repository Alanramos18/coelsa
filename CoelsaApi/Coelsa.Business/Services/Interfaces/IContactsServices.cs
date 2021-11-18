using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Coelsa.Data.Entities;
using Coelsa.Dto;

namespace Coelsa.Business.Services.Interfaces
{
    public interface IContactsServices
    {
        /// <summary>
        ///     Create a contact
        /// </summary>
        /// <param name="contactsDto">Contact dto</param>
        /// <param name="cancellationToken">Cancellation Transaction Token</param>
        /// <returns></returns>
        Task CreateAsync(ContactsDto contactsDto, CancellationToken cancellationToken);

        /// <summary>
        ///     Update a contact
        /// </summary>
        /// <param name="id">Id of contact</param>
        /// <param name="contactsDto">Contact dto</param>
        /// <param name="cancellationToken">Cancellation Transaction Token</param>
        /// <returns></returns>
        Task UpdateAsync(int id, ContactsDto contactsDto, CancellationToken cancellationToken);

        /// <summary>
        ///     Delete a contact
        /// </summary>
        /// <param name="id">Id of contact</param>
        /// <param name="cancellationToken">Cancellation Transaction Token</param>
        /// <returns></returns>
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        
        /// <summary>
        ///     Get all contacts ordered by company
        /// </summary>
        /// <param name="cancellationToken">Cancellation Transaction Token</param>
        /// <returns></returns>
        Task<IEnumerable<Contacts>> GetAllAsync(CancellationToken cancellationToken);
    }
}
