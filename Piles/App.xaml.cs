﻿using Microsoft.EntityFrameworkCore;
using Piles.DbContexts;
using Piles.Services;
using Piles.ViewModels;
using System.Windows;

namespace Piles
{
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Data Source=piles.db";
        private readonly IPilesDbContextFactory _pilesDbContextFactory;
        private readonly MainWindow _mainWindow;

        public App()
        {
            _pilesDbContextFactory = new PilesDbContextFactory(CONNECTION_STRING);
            IPileService pileService = new PileService(_pilesDbContextFactory);
            _mainWindow = new MainWindow()
            {
                DataContext = PileupViewModel.CreateViewModel(pileService)
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
