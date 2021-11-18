using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coelsa.Business.Services.Interfaces;
using Coelsa.Data.Entities;
using Coelsa.Dto;
using Coelsa.Web.Exceptions;
using Coelsa.Web.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Coelsa.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly IContactsServices _contactsServices;

        public ContactsController(IContactsServices contactsServices, ILogger<ContactsController> logger)
        {
            _contactsServices = contactsServices ?? throw new ArgumentNullException(nameof(contactsServices));
            _logger = logger;
        }


        /// <summary>
        ///     Create a Contact
        /// </summary>
        /// <param name="contactsDto">Contact dto</param>
        /// <param name="cancellationToken">Cancellation Transaction Token</param>
        /// <returns></returns>
        [HttpPost]
        [Route("/create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        public async Task<ActionResult> CreateAsync([FromBody]ContactsDto contactsDto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (contactsDto == null)
                    return new BadRequestObjectResult("No puede dejar los campos vacios.");

                ContactsValidation.ValidContact(contactsDto);

                await _contactsServices.CreateAsync(contactsDto, cancellationToken);
                
                return new OkObjectResult("Creado correctamente.");
            }
            catch (ContactsException e)
            {
                _logger.LogInformation(e.Message);

                return new BadRequestObjectResult(e.Message);
            }
        }

        /// <summary>
        ///     Update a Contact
        /// </summary>
        /// <param name="id">Id of contact</param>
        /// <param name="contactsDto">Contact dto</param>
        /// <param name="cancellationToken">Cancellation Transaction Token</param>
        /// <returns></returns>
        [HttpPut]
        [Route("/update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        public async Task<ActionResult> UpdateAsync([FromQuery] int id, ContactsDto contactsDto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (contactsDto == null)
                    return new BadRequestObjectResult("No puede dejar los campos vacios.");

                ContactsValidation.ValidContact(contactsDto);

                await _contactsServices.UpdateAsync(id, contactsDto, cancellationToken);

                return new OkObjectResult("Se actualizo correctamente.");
            }
            catch (ContactsException e)
            {
                _logger.LogInformation(e.Message);

                return new BadRequestObjectResult(e.Message);
            }
        }

        /// <summary>
        ///     Delete a Contact
        /// </summary>
        /// <param name="id">Id of contact</param>
        /// <param name="cancellationToken">Cancellation Transaction Token</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        public async Task<ActionResult> DeleteAsync([FromQuery] int id, CancellationToken cancellationToken = default)
        {
            await _contactsServices.DeleteAsync(id, cancellationToken);

            return new OkObjectResult("Se elimino correctamente.");
        }

        /// <summary>
        ///     Get all Contacts ordered by company
        /// </summary>
        /// <param name="cancellationToken">Cancellation Transaction Token</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/company")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Contacts>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await _contactsServices.GetAllAsync(cancellationToken);

            if (result == null || result.Count() == 0)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(result);
        }
    }
}
