using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Coelsa.Data.Entities;
using Coelsa.Dto;
using Dapper;
using Microsoft.Extensions.Options;

namespace Coelsa.Data
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly string _connection;
        
        public ContactsRepository(IOptions<DbConfig> config)
        {
            _connection = config.Value.ConnectionStrings;
        }

        public async Task CreateAsync(ContactsDto contactsDto, CancellationToken cancellationToken)
        {
            using (var db = new SqlConnection(_connection))
            {
                var sql = "insert into CONTACTS(firstName, lastName, company, email, phoneNumber) values (@firstName, @lastName, @company, @email, @phoneNumber)";
                await db.ExecuteAsync(sql, new {
                    firstName = contactsDto.FirstName,
                    lastName = contactsDto.LastName,
                    company = contactsDto.Company,
                    email = contactsDto.Email,
                    phoneNumber = contactsDto.PhoneNumber
                });
            }
        }

        public async Task UpdateAsync(int id, ContactsDto contactsDto, CancellationToken cancellationToken)
        {
            using (var db = new SqlConnection(_connection))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("update CONTACTS set ");
                builder.Append("firstName = @firstName, ");
                builder.Append("lastName = @lastName, ");
                builder.Append("company = @company, ");
                builder.Append("email = @email, ");
                builder.Append("phoneNumber = @phoneNumber ");
                builder.Append("where id = @id;");

                var sql = builder.ToString(); 
                await db.ExecuteAsync(sql, new {
                    firstName = contactsDto.FirstName,
                    lastName = contactsDto.LastName,
                    company = contactsDto.Company,
                    email = contactsDto.Email,
                    phoneNumber = contactsDto.PhoneNumber,
                    id = id
                });
            }
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            using (var db = new SqlConnection(_connection))
            {
                var sql = "delete from CONTACTS where @id = id;";
                await db.ExecuteAsync(sql, new { id = id });
            }
        }

        public async Task<IEnumerable<Contacts>> GetAllAsync(CancellationToken cancellationToken)
        {
            using (var db = new SqlConnection(_connection))
            {
                var sql = "select * from CONTACTS order by company asc;";
                var result = await db.ExecuteAsync(sql);
                var list = db.Query<Contacts>(sql);

                return list;
            }
        }
    }
}
