using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ATM_DAL.Data
{
    public class AtmDbContextFactory : IDesignTimeDbContextFactory<AtmDbContext>
    {


        public AtmDbContextFactory()
        {

        }


        public async Task<AtmDbContext> CreateDbContextAsync(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AtmDbContext>();

            var connectionString = @"Data Source=DESKTOP-HTUFPR1\SQLEXPRESS; Initial Catalog=AtmDB; Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            optionsBuilder.UseSqlServer(connectionString);

            //Console.WriteLine(connectionString);

            return new AtmDbContext(optionsBuilder.Options);
        }

        public AtmDbContext CreateDbContext(string[] args)
        {
            return CreateDbContextAsync(args).GetAwaiter().GetResult();
        }
    }
}
