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
    public class PlanPolizaTest
    {
        private MockRepository mockRepository;
        private Mock<IPlanPolizaService> mockService;

        [SetUp]
        public void Setup()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
            mockService = this.mockRepository.Create<IPlanPolizaService>();
        }

        public PlanPolizaController CreateService()
        {
            return new PlanPolizaController(mockService.Object);
        }

        [Test]
        public async Task GetByFilters()
        {
            var service = CreateService();

            var response = new ResponseListItem<PlanPolizaDTO>
            {
                CountItems = 1,
                ListItems = new List<PlanPolizaDTO>()
            };

            this.mockService.Setup(s => s.GetByFilters(It.IsAny<PlanPolizaByFiltersRequest>())).ReturnsAsync(response);

            PlanPolizaByFiltersRequest filters = new()
            {
                PageIndex = 1,
                PageSize = 5,
                NombrePlan = "",
                ValorMaximo = 5
            };
            var result = await service.GetByFilters(filters);
            NUnit.Framework.Assert.IsTrue(result?.Value?.Success);
        }
    }
}
