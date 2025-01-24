using AVS.NerdStore.Site.MVC;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Environment);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app);

app.Run();
