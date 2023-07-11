using Moq;
using NUnit.Framework;
using PersonalSoft.Api.Controllers;
using PersonalSoft.Domain.DTO;
using PersonalSoft.Domain.Request;
using PersonalSoft.Domain.Response;
using PersonalSoft.Service;

namespace PersonalSoft.UnitTest
{
    [TestFixture]
    public class ClienteTest
    {
        private MockRepository mockRepository;
        private Mock<IClienteService> mockClienteService;

        [SetUp]
        public void Setup()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
            mockClienteService = this.mockRepository.Create<IClienteService>();
        }

        public ClienteController CreateService()
        {
            return new ClienteController(mockClienteService.Object);
        }

        [Test]
        public async Task GetByFilters()
        {
            var service = CreateService();

            var response = new ResponseListItem<ClienteResultDTO>
            {
                CountItems = 1,
                ListItems = new List<ClienteResultDTO>()
            };

            this.mockClienteService.Setup(s => s.GetByFilters(It.IsAny<ClienteByFiltersRequest>())).ReturnsAsync(response);

            ClienteByFiltersRequest filters = new()
            {
                PageIndex = 1,
                PageSize = 5,
                Nombre = "",
                Identificacion = ""
            };
            var result = await service.GetByFilters(filters);
            NUnit.Framework.Assert.IsTrue(result?.Value?.Success);
        }
    }
}