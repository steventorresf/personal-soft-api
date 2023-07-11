using Moq;
using NUnit.Framework;
using PersonalSoft.Api.Controllers;
using PersonalSoft.Domain.DTO;
using PersonalSoft.Domain.Request;
using PersonalSoft.Domain.Response;
using PersonalSoft.Service;
using Assert = NUnit.Framework.Assert;

namespace PersonalSoft.UnitTest
{
    [TestFixture]
    public class PolizaTest
    {
        private MockRepository mockRepository;
        private Mock<IPolizaService> mockService;

        [SetUp]
        public void Setup()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
            mockService = this.mockRepository.Create<IPolizaService>();
        }

        public PolizaController CreateService()
        {
            return new PolizaController(mockService.Object);
        }

        [Test]
        public async Task GetByFilters()
        {
            var service = CreateService();

            var response = new ResponseListItem<PolizaResultDTO>();

            this.mockService.Setup(s => s.GetByFilters(It.IsAny<PolizaByFiltersRequest>())).ReturnsAsync(response);

            PolizaByFiltersRequest filters = new()
            {
                PageIndex = 1,
                PageSize = 5,
                Placa = "",
                Poliza = ""
            };
            var result = await service.GetByFilters(filters);
            Assert.IsTrue(result?.Value?.Success);
        }
    }
}
