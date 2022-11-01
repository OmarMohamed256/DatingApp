using System.Text;
using DatingApp.API.Data;
using DatingApp.API.Interfaces;
using DatingApp.API.Middleware;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using DatingApp.API.Helpers;

internal class Program
{
    private static async Task Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);
        // using var scope = builder.Services.BuildServiceProvider().CreateScope();
        // var services = scope.ServiceProvider;
        // try
        // {
        //     var context = services.GetRequiredService<DataContext>();
        //     await context.Database.MigrateAsync();
        //     await Seed.SeedUsers(context);
        // }
        // catch (Exception ex)
        // {
        //     var logger = services.GetRequiredService<ILogger<Program>>();
        //     logger.LogError(ex, "An error occurred during migration");
        // }
        // builder.Services.AddScoped<IDbInitializer, DbInitializer>();

        // Add services to the container.

        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddControllers();
        builder.Services.AddCors();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
        builder.Services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        builder.Services.AddScoped<Seed>();



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            await app.UseItToSeedSqlServerAsync();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}