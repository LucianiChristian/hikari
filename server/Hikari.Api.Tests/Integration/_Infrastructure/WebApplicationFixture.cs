using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.MsSql;

namespace Hikari.Api.Tests.Integration._Infrastructure;

public class WebApplicationFixture : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer;
    
    public WebApplicationFixture()
    {
        _msSqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("StrongPassword123!")
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("ConnectionStrings:Database", _msSqlContainer.GetConnectionString());
    }

    public async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _msSqlContainer.StopAsync();
        
        await base.DisposeAsync();
    }
}