using System;
using System.Net.Mail;
using Coelsa.Dto;
using Coelsa.Web.Exceptions;

namespace Coelsa.Web.Validations
{
    public static class ContactsValidation
    {
        public static void ValidContact(ContactsDto contactsDto)
        {
            if(contactsDto.FirstName == null || contactsDto.LastName == null)
                throw new ContactsException("Se deben ingresar nombre y apellido");

            if(contactsDto.Company == null)
                throw new ContactsException("La compania no puede quedar vacia");

            if(contactsDto.PhoneNumber == null)
                throw new ContactsException("El numero de telefono no puede quedar vacio");

            if (contactsDto.Email != null)
                ValidEmail(contactsDto.Email);
            else
                throw new ContactsException("El email no puede quedar vacio");
        }

        private static void ValidEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
            }
            catch (FormatException)
            {
                throw new ContactsException("Ingrese un mail correcto");
            }
        }
    }
}
