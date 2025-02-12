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
            PilesDbContextFactory pilesDbContextFactory = new PilesDbContextFactory(CONNECTION_STRING);
            using (PilesDbContext dbContext = pilesDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            base.OnStartup(e);
        }
    }
}
