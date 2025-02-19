using Microsoft.Extensions.Hosting;

namespace Piles.HostBuilder
{
    public static class AddHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
        {
            return hostBuilder;
        }
    }
}
