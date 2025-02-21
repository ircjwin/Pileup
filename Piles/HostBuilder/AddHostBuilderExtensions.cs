using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Piles.ViewModels;

namespace Piles.HostBuilder
{
    public static class AddHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<PileupViewModel>();
            });
            return hostBuilder;
        }
    }
}
