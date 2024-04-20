using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using UnitOfWork.Core.Models;

namespace UnitOfWork.Infrastructure
{
    public class DbContextFactory : IDesignTimeDbContextFactory<DbContextClass>
    {
        public DbContextClass CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContextClass>();
            optionsBuilder.UseSqlServer("Data Source=.;database=UnitOfWorkDemoDB; User Id=sa;Password=123;MultipleActiveResultSets=True;");

            return new DbContextClass(optionsBuilder.Options);
        }//User id=sa;password=123;
    }
    public class DbContextClass : DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> contextOptions) : base(contextOptions)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder
 optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-0U3KQS1;Initial Catalog = UnitOfWorkDemoDB; User Id=sa;Password=123");
        }

        public DbSet<ProductDetails> Products { get; set; }
    }
}