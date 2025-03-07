using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Piles.Models;
using Piles.Services;
using Piles.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Piles.HostBuilder
{
    public static class AddHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton(async (s) => await CreatePileupViewModel(s));

                services.AddTransient<PileViewModel>();
                services.AddTransient<Func<Pile, PileViewModel>>((s) => (pile) => new PileViewModel(pile, s.GetRequiredService<IRuminationService>()));
            });
            return hostBuilder;
        }

        private static async  Task<PileupViewModel> CreatePileupViewModel(IServiceProvider services)
        {
            IPileService pileService = services.GetService<IPileService>();
            ICollection<Pile> piles = await pileService.GetAllPiles();
            Pileup pileup = new Pileup(piles);
            return new PileupViewModel(pileup);
        }
    }
}
