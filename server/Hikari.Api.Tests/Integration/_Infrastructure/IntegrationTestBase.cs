using Hikari.Api.Database;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Hikari.Api.Tests.Integration._Infrastructure;

[CollectionDefinition(nameof(SharedFixture))]
public class SharedFixture : ICollectionFixture<WebApplicationFixture>;

[Collection(nameof(SharedFixture))]
public class IntegrationTestBase(WebApplicationFixture fixture) : IAsyncLifetime
{
    private IServiceScope? _scope;
    private IDbContextTransaction? _transaction;

    public AppDbContext? DbContext; 
    
    public async Task InitializeAsync()
    {
        _scope = fixture.Services.CreateScope();
        DbContext = _scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        _transaction = await DbContext.Database.BeginTransactionAsync();
    }

    public async Task DisposeAsync()
    {
        if (_transaction is not null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
        
        _scope?.Dispose();
    }
}