using System;
using System.Linq;
using AgendaApi.Models;
using AgendaApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace AgendaApi.Controllers
{
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaRepositories agendaRepository;
        public AgendaController(IAgendaRepositories agendaRepository)
            => this.agendaRepository = agendaRepository;    
        

        //Apresenta todos registros
        [HttpGet("/api/contatos")]
        public IActionResult GetAllAgenda()
        {
            return Ok(agendaRepository.GetAll());
        }

        //Pesquisa registro por id
        [HttpGet("/api/contatos/{id}")]
        public IActionResult GetAgendaById(int id)
        {
            var agenda = agendaRepository.GetById(id);

            if (agenda == null)
            {
                return NotFound();
            }
            return Ok(agenda);
        }


        //Cadastra novo registro
        [HttpPost("/api/contatos/")]
        public IActionResult CreateAgenda([FromBody] Agenda registro)
        {
            //Verifica se campos estão válidos
            if (registro == null ||
                string.IsNullOrWhiteSpace(registro.Nome) ||
                string.IsNullOrWhiteSpace(registro.Telefone))
            {
                return BadRequest();
            }

            var existingAgenda = agendaRepository.GetById(registro.Id);
            if (existingAgenda != null)
                return Conflict(registro);


            registro = agendaRepository.Create(registro);
            return Created();
        }

        //Atualiza registro
        [HttpPut("/api/contatos/{id}")]
        public IActionResult CreateAgenda([FromBody] Agenda registro, int id)
        {
            if (id != registro.Id)
                return Conflict();

            var existingAgenda = agendaRepository.GetById(registro.Id);
            if (existingAgenda == null)
                return NotFound();

            var retorno = agendaRepository.Update(existingAgenda, registro);

            return Ok(retorno);
        }

        //Exclui registro
      
        [HttpDelete("/api/contatos/{id}")]
        public IActionResult Delete(int id)
        {
            var existingAgenda = agendaRepository.GetById(id);
            if (existingAgenda == null)
                return NotFound();

            agendaRepository.Delete(existingAgenda);
            return NoContent();

        }



        //Pesquisa registro por nome
        //Pesquisa registro por nome
        [HttpGet("/api/contatos/pesquisar/{nome}")]
        public IActionResult GetAgendaByNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return BadRequest();

            var registros = agendaRepository
                .GetAll()
                .Where(a => a.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (registros == null || !registros.Any())
                return NotFound();

            return Ok(registros);
        }




    }
}
