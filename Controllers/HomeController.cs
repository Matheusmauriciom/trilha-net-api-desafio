using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TarefaAPI.Data;
using TarefaAPI.Models;

namespace TarefaAPI.Controllers
{   [ApiController]
   [Route("/tarefa")]
    public class HomeController : ControllerBase
    {
        //Endpoint GET(READ) para listar todas tarefas.
        [HttpGet]
        public IActionResult Get([FromServices] AppDbContext context) => Ok(context.Tarefas.ToList());

        //Endpoint GET(READ) para buscar pelo ID.
        [HttpGet("{id:int}")]
        public IActionResult GetById([FromServices] AppDbContext context, int id)
        {   // Busca no banco de dados
            var tarefa = context.Tarefas.FirstOrDefault(x => x.Id == id);

            if(tarefa == null)
            return NotFound();

            return Ok(tarefa);
        }

        //Endpoint GET(READ) para buscar pelo Titulo.
        [HttpGet("titulo/{titulo}")]
        public IActionResult GetByTitle([FromServices] AppDbContext context, string titulo)
        {
            var tarefa = context.Tarefas.FirstOrDefault(x => x.Titulo == titulo);

            if(tarefa == null)
            return NotFound();

            return Ok (tarefa);
        }

        //Endpoint GET(READ) buscar pela Data.
        [HttpGet("data/{data}")]
        public IActionResult GetByDate([FromServices] AppDbContext context, string data)
        {
            if(!DateTime.TryParse(data, out DateTime dataConvertida))
            {
                return BadRequest("Formato de data inválido");
            }
            var tarefas = context.Tarefas.Where(x => x.Data == dataConvertida).ToList();

              if (tarefas == null || tarefas.Count == 0)
            return NotFound();       

            return Ok(tarefas);     
        }

        //Endpoint GET(READ) para buscar pelo status.
        [HttpGet("status/{status}")]
        public IActionResult GetByStatus([FromServices] AppDbContext context, StatusTarefa status)
        {
            var tarefa = context.Tarefas.FirstOrDefault(x => x.Status == status);


            if (tarefa == null)
                return NotFound();


            return Ok(tarefa);
        }

        //Endpoint POST(CREATE) uma nova tarefa
        [HttpPost]
        public IActionResult Post([FromServices] AppDbContext context, [FromBody] TarefaModels tarefa)
        {
            context.Add(tarefa);
            context.SaveChanges();

             return Created($"/tarefa/{tarefa.Id}", tarefa);
        }


        //Endpoint PUT(UPDATE) atualizar a tarefa
        [HttpPut("{id:int}")]
        public IActionResult Put (int id, [FromServices] AppDbContext context, [FromBody] TarefaModels tarefa)
        {
            var atualizarTarefa = context.Tarefas.FirstOrDefault(x => x.Id == id);
             if (atualizarTarefa == null)
            return NotFound();   

             atualizarTarefa.Titulo = tarefa.Titulo;
            atualizarTarefa.Descricao = tarefa.Descricao;
            atualizarTarefa.Status = tarefa.Status;
            atualizarTarefa.Data = tarefa.Data;

            context.Tarefas.Update(atualizarTarefa);
            context.SaveChanges();
            return Ok(atualizarTarefa);
        }
    
        //Endpoint Delete para excluir uma tarefa.
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id, [FromServices] AppDbContext context)
        {
            var tarefa = context.Tarefas.FirstOrDefault(x => x.Id == id);
            if (tarefa == null)
                return NotFound();

            context.Tarefas.Remove(tarefa);
            context.SaveChanges();

            return Ok(tarefa);
        }

    }
}