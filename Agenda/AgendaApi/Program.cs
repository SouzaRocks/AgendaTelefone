
using AgendaApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace AgendaApi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddControllers();
            builder.Services.AddScoped<IAgendaRepositories, AgendaRepository>();
            builder.Services.AddDbContext<AgendaDbContext>();
            var app = builder.Build();
            app.MapControllers();
            app.Run();

        }
    }
}
