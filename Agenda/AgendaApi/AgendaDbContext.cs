using AgendaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaApi
{
    public class AgendaDbContext : DbContext
    {
        public DbSet<Agenda> Registros => Set<Agenda>();


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=c:\\caixa\\caixa.db");
            
            base.OnConfiguring(optionsBuilder);
        }


    }
}
