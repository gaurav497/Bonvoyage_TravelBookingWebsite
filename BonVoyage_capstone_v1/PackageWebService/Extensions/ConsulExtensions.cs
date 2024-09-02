using Consul;

namespace PackageWebService.Extensions
{
    public static class ConsulExtensions
    {

        public static IServiceCollection AddConsul(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                consulConfig.Address = new Uri(configuration["Consul:Address"]!);
            }));

            return services;
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app)
        {
            var configuration = app.ApplicationServices.GetService<IConfiguration>();
            var consulClient = app.ApplicationServices.GetService<IConsulClient>();
            var registration = new AgentServiceRegistration()
            {
                ID = configuration["Consul:Service:ID"],
                Name = configuration["Consul:Service:Name"],
                Address = configuration["Consul:Service:Address"],
                Port = int.Parse(configuration["Consul:Service:Port"]),
                Tags = configuration.GetSection("Consul:Service:Tags").Get<string[]>(),
               
            };

            // Deregister the service if it was previously registered
            consulClient.Agent.ServiceDeregister(registration.ID).Wait();

            // Register the service
            consulClient.Agent.ServiceRegister(registration).Wait();

            // Deregister the service when the application is stopping
            var lifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();
            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            });

            return app;
        }

    }
}