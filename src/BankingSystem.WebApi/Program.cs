using BankingSystem.WebApi.Configurations.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddWebConfig(builder.Configuration);

var app = builder.Build();
app.UseWebConfig();

app.Run();