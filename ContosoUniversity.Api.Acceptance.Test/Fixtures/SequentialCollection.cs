using Xunit;

namespace ContosoUniversity.Api.Acceptance.Test.Fixtures
{
    [CollectionDefinition("Sequential")]
    public class SequentialCollection: ICollectionFixture<AcceptanceTestFixture>
    {
    }
}
