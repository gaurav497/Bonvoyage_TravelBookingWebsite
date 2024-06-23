
using UserService.Extensions;
using UserService.Repository;

namespace UserService
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
            builder.Services.AddScoped< IUserRepo, UserRepo>();
            builder.Services.AddHttpClient("booking-service", client => {
                client.BaseAddress = new Uri("https://localhost:7201/"); 
                                                                         
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
