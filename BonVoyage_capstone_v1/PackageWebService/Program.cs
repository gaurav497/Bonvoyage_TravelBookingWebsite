

using PackageWebService.Extensions;
using PackageWebService.Repository;

namespace PackageWebService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
           
         
            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddConsul(configuration);
           builder.Services.AddScoped<IPackageRepo, PackageRepo>();
            builder.Services.AddHttpClient("package-service", client => {
                client.BaseAddress = new Uri("https://localhost:7159/"); 
                                                                         
            });
            var app = builder.Build();

         
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.UseConsul();
           
            app.Run();
        }
    }
}
