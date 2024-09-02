using Consul;

namespace UserService.ServiceDiscovery
{
    public static class ConsulServiceDiscovery
    {
        public static async Task<Uri> GetServiceUri(IConsulClient consulClient, string serviceName)
        {
            var services = await consulClient.Catalog.Service(serviceName);

            if (services.Response.Length == 0)
            {
                return null;
            }

           
            var serviceAddress = services.Response[0].ServiceAddress;
            var servicePort = services.Response[0].ServicePort;

            return new Uri($"https://{serviceAddress}:{servicePort}");
        }
    }
}
