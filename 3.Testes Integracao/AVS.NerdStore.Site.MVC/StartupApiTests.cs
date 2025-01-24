using System.Text;
using AVS.NerdStore.Catalogo.Application.AutoMapper;
using AVS.NerdStore.Catalogo.Data;
using AVS.NerdStore.Site.MVC.Data;
using AVS.NerdStore.Site.MVC.Models;
using AVS.NerdStore.Site.MVC.Setup;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AVS.NerdStore.Site.MVC;

public class StartupApiTests
{
    private readonly IConfiguration _configuration;

    public StartupApiTests(IHostEnvironment hostEnvironment)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(hostEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables();

        _configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        // services.Configure<CookiePolicyOptions>(options =>
        // {
        //     // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        //     options.CheckConsentNeeded = context => false;
        //     options.MinimumSameSitePolicy = SameSiteMode.None;
        // });

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddDbContext<CatalogoContext>(options =>
            options.UseSqlite(connectionString));

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddDefaultIdentity<IdentityUser>()
                 .AddRoles<IdentityRole>()
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddDefaultTokenProviders();

        var appSettingsSection = _configuration.GetSection("AppSettings");
        services.Configure<AppSettings>(appSettingsSection);

        var appSettings = appSettingsSection.Get<AppSettings>() ?? throw new InvalidOperationException("AppSettings is not configured.");

        if (string.IsNullOrEmpty(appSettings.Secret)) throw new InvalidOperationException("AppSettings.Secret is not configured.");

        var key = Encoding.ASCII.GetBytes(appSettings.Secret);

        // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //     .AddJwtBearer(options =>
        //     {
        //         options.RequireHttpsMetadata = false;
        //         options.SaveToken = true;
        //         options.TokenValidationParameters = new TokenValidationParameters
        //         {
        //             ValidateIssuerSigningKey = true,
        //             IssuerSigningKey = new SymmetricSecurityKey(key),
        //             ValidateIssuer = true,
        //             ValidateAudience = true,
        //             ValidAudience = appSettings.ValidoEm,
        //             ValidIssuer = appSettings.Emissor
        //         };
        //     });

        //services.AddControllersWithViews();

        //services.AddRazorPages();

        services.AddHttpContextAccessor();

        services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));

        services.AddMediatR(typeof(Startup));

        services.RegisterServices();

    }

    public void Configure(IApplicationBuilder app)
    {

        if (app.ApplicationServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        //app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Vitrine}/{action=Index}/{id?}");
            //endpoints.MapRazorPages();
        });

    }
}
