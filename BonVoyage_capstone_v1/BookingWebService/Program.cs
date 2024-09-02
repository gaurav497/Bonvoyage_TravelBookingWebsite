
using BookingWebService.Extensions;
using BookingWebService.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace BookingWebService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var configuration = builder.Configuration;
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddConsul(configuration);
            builder.Services.AddScoped<IBookingRepo, BookingRepo>();
            builder.Services.AddHttpClient("booking-service", client => {
                client.BaseAddress = new Uri("https://localhost:7160/"); // Replace with the actual base URL of the CustomerService
                                                                         // Additional HttpClient configurations if needed
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {

                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ClockSkew = TimeSpan.Zero,
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = "GlobalLogic",
                     ValidAudience = "GlobalLogic",
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MaraudersMapISolemnlySwearThatIAmUptoNoGood"))

                 };
             });
            var app = builder.Build();
        

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            app.UseConsul();

            app.Run();
        }
    }
}
