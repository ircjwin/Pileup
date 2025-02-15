using Microsoft.EntityFrameworkCore;
using Piles.Data;
using Piles.Services;
using System.Windows;

namespace Piles
{
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Data Source=piles.db";

        private readonly PilesDbContextFactory _pilesDbContextFactory;

        public App()
        {
            _pilesDbContextFactory = new PilesDbContextFactory(CONNECTION_STRING);

            // TODO: Stitch services to models
            IPileCreator pileCreator = new DatabasePileCreator(_pilesDbContextFactory);
            IPileProvider pileProvider = new DatabasePileProvider(_pilesDbContextFactory);
            IRuminationCreator ruminationCreator = new DatabaseRuminationCreator(_pilesDbContextFactory);
            IRuminationProvider ruminationProvider = new DatabaseRuminationProvider(_pilesDbContextFactory);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (PilesDbContext dbContext = _pilesDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            base.OnStartup(e);
        }
    }
}
