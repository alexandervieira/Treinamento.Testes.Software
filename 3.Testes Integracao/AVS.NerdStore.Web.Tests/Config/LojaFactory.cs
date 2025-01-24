using System.Security.Claims;
using System.Text.Encodings.Web;
using AVS.NerdStore.Site.MVC;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AVS.NerdStore.Site.MVC.Data;
using AVS.NerdStore.Catalogo.Data;


namespace AVS.NerdStore.Web.Tests.Config;

public class LojaFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        //builder.UseStartup<TStartup>();

         builder.ConfigureServices(services =>
        {
            // Evitar duplicação de esquemas de autenticação
            // services.AddAuthentication(options =>
            // {
            //     options.DefaultAuthenticateScheme = "TestScheme";
            //     options.DefaultChallengeScheme = "TestScheme";
            // }).AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>("TestScheme", options => { });

            // Substituir a string de conexão por um banco de dados em memória (opcional para testes)
            // var serviceProvider = new ServiceCollection()
            //     .AddEntityFrameworkInMemoryDatabase()
            //     .BuildServiceProvider();

            // services.AddDbContext<ApplicationDbContext>(options =>
            // {
            //     options.UseInMemoryDatabase("TestDb");
            //     options.UseInternalServiceProvider(serviceProvider);
            // });

            // services.AddDbContext<CatalogoContext>(options =>
            // {
            //     options.UseInMemoryDatabase("TestDb");
            //     options.UseInternalServiceProvider(serviceProvider);
            // });
        });

        builder.UseEnvironment("Development"); // Ambiente de teste
       
    }

}

public class TestAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{

    public TestAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
                                     ILoggerFactory logger,
                                     UrlEncoder encoder) : base(options, logger, encoder) { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Simula um usuário autenticado sempre que o handler for chamado
        var claims = new[] { new Claim(ClaimTypes.Name, "TestUser") };
        var identity = new ClaimsIdentity(claims, "TestScheme");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "TestScheme");

        // Retorna o resultado da autenticação simulada
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
