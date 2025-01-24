using System.Net.Http.Json;
using System.Text.RegularExpressions;
using AVS.NerdStore.Site.MVC;
using AVS.NerdStore.Site.MVC.Models;
using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;

namespace AVS.NerdStore.Web.Tests.Config
{
    [CollectionDefinition(nameof(IntegrationWebTestsFixtureCollection))]
    public class IntegrationWebTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupWebTests>> { }
    
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupApiTests>> { }
    
    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public LojaFactory<TStartup> Factory { get; private set; }
        public HttpClient Client { get; private set; }
        public string UsuarioToken { get; private set; }
        public string UsuarioEmail { get; set; }
        public string UsuarioSenha { get; set; }
        public string AntiForgeryFieldName = "__RequestVerificationToken";

        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("http://localhost"),
                HandleCookies = true,
                MaxAutomaticRedirections = 7                
            };

            Factory = new LojaFactory<TStartup>();
            Client = Factory.CreateClient(clientOptions);
            UsuarioToken = string.Empty;
            UsuarioEmail = string.Empty;
            UsuarioSenha = string.Empty;
        }

        public void GerarUserSenha()
        {
            var faker = new Faker("pt_BR");
            UsuarioEmail = faker.Internet.Email().ToLower();
            UsuarioSenha = faker.Internet.Password(8, false, "", "@1Ab_");
        }

        public async Task RealizarLoginApi()
        {
            var userData = new LoginViewModel
            {
                Email = "teste@gmail.com",
                Senha = "Teste@123"
            };

            // Recriando o client para evitar configurações de Web
            Client = Factory.CreateClient();

            var response = await Client.PostAsJsonAsync("api/identidade/autenticar", userData);
            response.EnsureSuccessStatusCode();
            UsuarioToken = await response.Content.ReadAsStringAsync();
        }

        public async Task RealizarLoginWeb()
        {
            var response = await Client.GetAsync("Identity/Account/Login");
            response.EnsureSuccessStatusCode();

            var antiForgeryToken = ObterAntiForgeryToken(await response.Content.ReadAsStringAsync());

            var formData = new Dictionary<string, string>
            {
                {AntiForgeryFieldName, antiForgeryToken},
                {"Input.Email", "teste@gmail.com"},
                {"Input.Password", "Teste@123"}
            };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "Identity/Account/Login")
            {
                Content = new FormUrlEncodedContent(formData)
            };

            await Client.SendAsync(postRequest);
        }

        public string ObterAntiForgeryToken(string htmlBody)
        {
            var requestVerificationTokenMatch = Regex.Match(htmlBody, $@"\<input name=""{AntiForgeryFieldName}"" type=""hidden"" value=""([^""]+)"" \/\>");

            if (!requestVerificationTokenMatch.Success) throw new ArgumentException($"Anti forgery token '{AntiForgeryFieldName}' não encontrado no HTML");

            return requestVerificationTokenMatch.Groups[1].Captures[0].Value;
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }

}