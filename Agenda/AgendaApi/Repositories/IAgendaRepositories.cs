using System.Collections.Generic;
using AgendaApi.Models;

namespace AgendaApi.Repositories
{
    public interface IAgendaRepositories
    {
        IEnumerable<Agenda> GetAll();
        Agenda GetById(int id);
        Agenda Create(Agenda agenda);
        bool Update(Agenda existing, Agenda newAgenda);
        bool Delete(int id);

    }
}
