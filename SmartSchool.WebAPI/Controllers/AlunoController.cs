using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public List<Aluno> Alunos = new List<Aluno>() {
            new Aluno() {
                Id = 1,
                Nome = "Marcos",
                Sobrenome = "Almeida da Costa",
                Telefone = "1231546"
            },
            new Aluno() {
                Id = 2,
                Nome = "Marta",
                Sobrenome = "Kent",
                Telefone = "3123124"
            },
            new Aluno() {
                Id = 3,
                Nome = "Laura",
                Sobrenome = "Maria",
                Telefone = "12341556"
            }
        };

        public AlunoController() {}

        // api/aluno
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Alunos);
        }

        // api/aluno/byId/3
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = Alunos.FirstOrDefault(a => a.Id == id);
            
            if (aluno == null) return BadRequest("O Aluno não foi encontrado.");

            return Ok(aluno);
        }

        // api/aluno/nome
        [HttpGet("ByName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = Alunos.FirstOrDefault(a => a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome));
            
            if (aluno == null) return BadRequest("O Aluno não foi encontrado.");

            return Ok(aluno);
        }

        // api/aluno
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            return Ok(aluno);
        }

        // api/aluno/1
        // utilizado para atualizar um registro (dar um update)
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        // api/aluno/1
        // atualizar um registro parcialmente
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        // api/aluno
        // deletar um registro
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}