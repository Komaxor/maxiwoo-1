using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Mxc.IBSDiscountCard.Infrastructure.Repositories
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseNpgsql("User ID=sa;Password=foReVermine930;Host=localhost;Port=1401;Database=DB_A4E582_ibs;");
            
            return new ApplicationDbContext(optionsBuilder.Options, null);
        }
    }
}