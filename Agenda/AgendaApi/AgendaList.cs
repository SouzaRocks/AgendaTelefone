using System.Collections.Generic;
using AgendaApi.Models;

namespace AgendaApi
{
    public class AgendaList
    {
        public static Dictionary<int, Agenda> Registros { get; set; }

        static AgendaList()
        {
            Registros = new()
            {
                { 1, new() {Id = 1, Nome = "Nome 1", Telefone = "99999991"} },
                { 2, new() {Id = 2, Nome = "Nome 2", Telefone = "99999992"} }

            };
        }
    }
}
