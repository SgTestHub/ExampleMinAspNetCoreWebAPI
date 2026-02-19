using  Microsoft.EntityFrameworkCore;
namespace TestAPIs
{
    public class TestDB:DbContext
    {
        public TestDB(DbContextOptions<TestDB> options) : base(options)
        {
        }
        public DbSet<TestModelItem> TestItem { get; set; }
    }
}
