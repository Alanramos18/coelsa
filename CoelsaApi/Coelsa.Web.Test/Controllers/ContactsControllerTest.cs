using System.Threading;
using System.Threading.Tasks;
using Coelsa.Business.Services.Interfaces;
using Coelsa.Dto;
using Coelsa.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Telerik.JustMock;

namespace Coelsa.Web.Test.Controllers
{
    [TestFixture]
    public class ContactsControllerTest
    {
        private IContactsServices _contactsServices;
        private ILogger<ContactsController> _logger;
        private CancellationToken _cancellationToken;

        private ContactsController _controller;

        [OneTimeSetUp]
        public void Setup()
        {
            _contactsServices = Mock.Create<IContactsServices>(Behavior.Strict);
            _logger = Mock.Create<ILogger<ContactsController>>();
            _cancellationToken = new CancellationToken();

            _controller = new ContactsController(_contactsServices, _logger);
        }

        [Test]
        public async Task CreateAsync_WithValidDto_ReturnsOk()
        {
            //Arrange
            var dto = new ContactsDto
            {
                FirstName = "Alan",
                LastName = "Ramos",
                Email = "alanramos_18@hotmail.com",
                Company = "Shinra",
                PhoneNumber = "1123123123"
            };

            Mock.Arrange(() => _contactsServices.CreateAsync(dto, _cancellationToken)).Returns(Task.CompletedTask);

            //Act
            var result = await _controller.CreateAsync(dto, _cancellationToken);

            //Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task CreateAsync_WithInvalidDto_ReturnsBadRequest()
        {
            //Arrange
            ContactsDto dto = null;

            //Act
            var result = await _controller.CreateAsync(dto, _cancellationToken);

            //Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo("No puede dejar los campos vacios."));
        }
    }
}