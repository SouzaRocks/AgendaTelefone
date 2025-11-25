
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
            var app = builder.Build();
            app.MapControllers();
            app.Run();

        }
    }
}
