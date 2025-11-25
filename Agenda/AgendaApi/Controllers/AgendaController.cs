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

        //Pesquisa registro por nome
        [HttpGet("/api/contatos/pesquisar/{nome}")]
        public IActionResult GetAgendaByNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return BadRequest();

            var registros = agendaRepository
                .GetAll()
                .Where(a => a.Nome != null &&
                            a.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!registros.Any())
                return NotFound();

            return Ok(registros);
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

            //Verifica se já existe contato com mesmo nome (case-insensitive)
            var existingAgendaByNome = agendaRepository
                .GetAll()
                .FirstOrDefault(a => a.Nome != null &&
                                     a.Nome.Equals(registro.Nome, StringComparison.OrdinalIgnoreCase));

            if (existingAgendaByNome != null)
            {
                return Conflict();
            }

            //Verifica se já existe contato com mesmo Id
            var existingAgendaById = agendaRepository.GetById(registro.Id);
            if (existingAgendaById != null)
            {
                return Conflict();
            }

            registro = agendaRepository.Create(registro);
            return Ok(registro);
        }

        //Atualiza registro
        [HttpPut("/api/contatos/{id}")]
        public IActionResult UpdateAgenda([FromBody] Agenda registro, int id)
        {
            if (id != registro.Id)
                return Conflict();

            var existingAgenda = agendaRepository.GetById(registro.Id);
            if (existingAgenda == null)
                return NotFound();

            // Verifica se já existe outro contato com o mesmo nome
            var nomeDuplicado = agendaRepository
                .GetAll()
                .Any(a => a.Id != registro.Id &&
                          a.Nome != null &&
                          a.Nome.Equals(registro.Nome, StringComparison.OrdinalIgnoreCase));

            if (nomeDuplicado)
                return Conflict();

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

            agendaRepository.Delete(id);
            return NoContent();

        }







    }
}
