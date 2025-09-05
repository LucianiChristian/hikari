using Hikari.Api.Database;
using Microsoft.EntityFrameworkCore;

namespace Hikari.Api;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetRequiredSection("ConnectionStrings")["Database"];
        
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        return builder;
    }
}