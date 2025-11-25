using System;
using System.Linq;
using AgendaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApi.Controllers
{
    public class AgendaController : ControllerBase
    {
        
        public AgendaController(AgendaDbContext dbContext)
        {
            
        }

        //Apresenta todos registros
        [HttpGet]
        [Route("/api/contatos")]
        public IActionResult GetAllAgenda()
        {
            return Ok(AgendaList.Registros);
        }

        //Pesquisa registro por id
        [HttpGet]
        [Route("/api/contatos/{id}")]
        public IActionResult GetAgendaById(int id)
        {
            if (!AgendaList.Registros.ContainsKey(id))
            {
                return NotFound();
            }
            return Ok(AgendaList.Registros[id]);
        }

        //Pesquisa registro por nome
        [HttpGet]
        [Route("/api/contatos/nome/{nome}")]
        public IActionResult GetAgendaByName(string nome)
        {
            var registro = AgendaList.Registros
                .Values
                .FirstOrDefault(r => r.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

            if (registro == null)
                return NotFound();

            return Ok(registro);
        }

        //Cadastra novo registro
        [HttpPost]
        [Route("/api/contatos/")]
        public IActionResult CreateAgenda([FromBody] Agenda registro)
        {
            //Verifica se campos estão válidos
            if (registro == null ||
                string.IsNullOrWhiteSpace(registro.Nome) ||
                string.IsNullOrWhiteSpace(registro.Telefone))
            {
                return BadRequest();
            }

            if (AgendaList.Registros.ContainsKey(registro.Id))
                return Conflict(registro);
            //Verifica se já consta nome na lista
            if (AgendaList.Registros.Values.Any(r => r.Nome.Equals(registro.Nome, System.StringComparison.OrdinalIgnoreCase)))
            {
                return Conflict();
            }


            AgendaList.Registros.Add(registro.Id, registro);

            return Created();
        }

        //Atualiza registro
        [HttpPut]
        [Route("/api/contatos/{id}")]
        public IActionResult CreateAgenda([FromBody] Agenda registro, int id)
        {
            if (id != registro.Id)
                return BadRequest();

            if (!AgendaList.Registros.ContainsKey(registro.Id))
                return NotFound();
            //Verifica se já consta nome na lista
            if (AgendaList.Registros.Values.Any(r =>
                r.Id != id && r.Nome.Equals(registro.Nome, StringComparison.OrdinalIgnoreCase)))
            {
                return Conflict();
            }


            var existing = AgendaList.Registros[id];

            existing.Nome = registro.Nome;
            existing.Telefone = registro.Telefone;
            
            AgendaList.Registros.Add(registro.Id, registro);

            return NoContent();
        }

        //Exclui registro
        [HttpDelete]
        [Route("/api/contatos/{id}")]
        public IActionResult Delete(int id)
        {
            if(!AgendaList.Registros.ContainsKey(id))
                return NotFound();

            AgendaList.Registros.Remove(id);

            return NoContent();
        }


    }
}
