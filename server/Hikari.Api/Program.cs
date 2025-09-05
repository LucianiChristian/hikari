using Hikari.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.AddDatabase();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public partial class Program;