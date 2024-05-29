
using Desafio.Api.Data;
using Desafio.Api.Interfaces;
using Desafio.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("Sqlite");

            builder.Services.AddDbContext<DesafioContext>(opt =>
            {
                opt.UseSqlite(connectionString);
            });

            builder.Services.AddScoped<IRepositoryProduct, RepositoryProduct>();
            builder.Services.AddScoped<IRepositoryCategory, RepositoryCategory>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
