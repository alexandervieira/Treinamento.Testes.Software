using AVS.Features.Tests;
using AVS.NerdStore.Site.MVC;
using AVS.NerdStore.Web.Tests.Config;

namespace AVS.NerdStore.Web.Tests
{
    [TestCaseOrderer("AVS.Features.Tests.PriorityOrderer", "AVS.Features.Tests")]
    [Collection(nameof(IntegrationWebTestsFixtureCollection))]
    public class VitrineControllerTests
    {
        private readonly IntegrationTestsFixture<StartupWebTests> _testsFixture;

        public VitrineControllerTests(IntegrationTestsFixture<StartupWebTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Acessar Vitrine com Sucesso"), TestPriority(1)]
        [Trait("Categoria", "Integração Web - VitrineController")]
        public async Task VitrineController_AcessarIndex_DeveRetornarSucesso()
        {
            // Arrange
            var reponse = await _testsFixture.Client.GetAsync("/Vitrine");
            
            // Asser
            reponse.EnsureSuccessStatusCode();
        }
        
    }
}