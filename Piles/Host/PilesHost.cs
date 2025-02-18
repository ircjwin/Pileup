using Microsoft.Extensions.Hosting;

namespace Piles.Host
{
    public static class AddHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
        {
            return hostBuilder;
        }
    }
}
