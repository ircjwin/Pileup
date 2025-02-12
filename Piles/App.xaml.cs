using Microsoft.EntityFrameworkCore;
using Piles.Data;
using System.Windows;

namespace Piles
{
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Data Source=piles.db";
        protected override void OnStartup(StartupEventArgs e)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(CONNECTION_STRING).Options;
            PilesDbContext dbContext = new PilesDbContext(options);
            dbContext.Database.Migrate();

            base.OnStartup(e);
        }
    }
}
