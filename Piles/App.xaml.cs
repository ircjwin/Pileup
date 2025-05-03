using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly ServiceProvider _services;

        public App()
        {
            _services = new ServiceCollection()
                .AddTransient<PileViewModel>()
                .AddTransient<RuminationViewModel>()

                .AddScoped<IPileService, PileService>()
                .AddScoped<IRuminationService, RuminationService>()

                .AddSingleton<IPilesDbContextFactory>(new PilesDbContextFactory(CONNECTION_STRING))
                .AddSingleton<CommandStackViewModel>((s) => new CommandStackViewModel(s.GetRequiredService<IPilesDbContextFactory>()))

                .AddSingleton<Func<Rumination, Pile, RuminationViewModel>>(
                    (s) => (r, p) => new RuminationViewModel(
                        r, 
                        p, 
                        s.GetRequiredService<CommandStackViewModel>()
                ))

                .AddSingleton<Func<Pile, PileViewModel>>(
                    (s) => (p) => new PileViewModel(
                        p, 
                        s.GetRequiredService<Func<Rumination, Pile, RuminationViewModel>>(), 
                        s.GetRequiredService<CommandStackViewModel>()
                ))

                .AddSingleton<PileupViewModel>(
                    (s) => PileupViewModel.CreateViewModel(
                        s.GetRequiredService<Func<Pile, PileViewModel>>(), 
                        s.GetRequiredService<IPileService>(), 
                        s.GetRequiredService<CommandStackViewModel>()
                ))

                .AddSingleton<MainViewModel>(
                    (s) => new MainViewModel(
                        s.GetRequiredService<PileupViewModel>(), 
                        s.GetRequiredService<CommandStackViewModel>()
                ))

                .AddSingleton<MainWindow>((s) => new MainWindow()
                {
                    DataContext = s.GetRequiredService<MainViewModel>()
                })
                .BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (PilesDbContext dbContext = _services.GetRequiredService<IPilesDbContextFactory>().CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            MainWindow = _services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
