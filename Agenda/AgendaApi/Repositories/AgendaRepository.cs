using System.Collections.Generic;
using AgendaApi.Models;


namespace AgendaApi.Repositories
{
    public class AgendaRepository : IAgendaRepositories
    {
        private readonly AgendaDbContext dbContext;

        public AgendaRepository(AgendaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Agenda> GetAll()
        {
            return dbContext.Registros;
        }


        public Agenda GetById(int id)
        {
            return dbContext.Registros.Find(id);
        }

        public Agenda Create(Agenda agenda)
        {
            dbContext.Registros.Add(agenda);
            dbContext.SaveChanges();

            return agenda;
        }

        public bool Update(Agenda existing, Agenda newAgenda)
        {
            existing.Nome = newAgenda.Nome;
            existing.Telefone = newAgenda.Telefone;
            
            dbContext.Registros.Remove(existing);
            dbContext.SaveChanges();

            return true;
        }


        public bool Delete(Agenda id)
        {
            var registro = dbContext.Registros.Find(id);
            if (registro != null)
            {
                dbContext.Registros.Remove(registro);
                dbContext.SaveChanges();
            }    
            
            return true;
        }



    }
}
