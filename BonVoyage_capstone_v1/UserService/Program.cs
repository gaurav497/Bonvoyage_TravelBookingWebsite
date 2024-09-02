
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserService.Extensions;
using UserService.Repository;

namespace UserService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
         

            var configuration = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();
            builder.Services.AddControllers();
        
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddConsul(configuration);
            builder.Services.AddScoped< IUserRepo, UserRepo>();
            //builder.Services.AddHttpClient("Users-Service", client => {
            //    client.BaseAddress = new Uri("https://localhost:7201/"); 

            //});
            builder.Services.AddHttpClient();

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
                     ValidIssuer = configuration["Jwt:Issuer"],
                     ValidAudience = configuration["Jwt:Issuer"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))

                 };
             });
            var app = builder.Build();
           
       
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
