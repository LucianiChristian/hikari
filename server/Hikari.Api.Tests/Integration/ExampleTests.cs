using Hikari.Api.Tests.Integration._Infrastructure;

namespace Hikari.Api.Tests.Integration;

public class ExampleTests(WebApplicationFixture webApplicationFixture) : IntegrationTestBase(webApplicationFixture)
{
    [Fact]
    public void Testing()
    {
        Assert.Fail("Testing...");
    }
}