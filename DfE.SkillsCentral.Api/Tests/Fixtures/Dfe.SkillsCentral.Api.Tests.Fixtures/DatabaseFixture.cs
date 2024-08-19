using Xunit;

namespace Dfe.SkillsCentral.Api.Tests.Fixtures
{
    public class DatabaseFixture
    {
        public DatabaseFixture()
        {
            
        }
    }

    [CollectionDefinition("DatabaseCollection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        // No code needed, this class only serves as an anchor for the collection
    }
}
