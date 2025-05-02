using Microsoft.EntityFrameworkCore;
using Piles.DbContexts;
using Piles.Models;
using Piles.Services;
using Piles.ViewModels;
using System;
using System.Windows;

namespace Piles
{
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Data Source=piles.db";
        private readonly IPilesDbContextFactory _pilesDbContextFactory = new PilesDbContextFactory(CONNECTION_STRING);
        private readonly MainWindow _mainWindow;

        public App()
        {
            IPileService pileService = new PileService(_pilesDbContextFactory);
            Func<Rumination, Pile, RuminationViewModel> createRuminationViewModel = (r, p) => new RuminationViewModel(r, p, CommandStackViewModel.Instance);
            Func<Pile, PileViewModel> createPileViewModel = (p) => new PileViewModel(p, createRuminationViewModel, CommandStackViewModel.Instance);

            _mainWindow = new MainWindow()
            {
                DataContext = PileupViewModel.CreateViewModel(createPileViewModel, pileService, CommandStackViewModel.Instance)
            };
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (PilesDbContext dbContext = _pilesDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            _mainWindow.Show();

            base.OnStartup(e);
        }
    }
}
