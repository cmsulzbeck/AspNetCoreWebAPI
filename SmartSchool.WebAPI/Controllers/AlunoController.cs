using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repo;

        public AlunoController(IRepository repo)
        {
            _repo = repo;
        }

        // api/aluno
        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllAlunos(true);

            return Ok(result);
        }

        // api/aluno/id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado.");

            return Ok(aluno);
        }

        // api/aluno
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repo.Add(aluno);
            if(_repo.SaveChanges()) return Ok(aluno);

            return BadRequest("Aluno não cadastrado");
        }

        // api/aluno/1
        // utilizado para atualizar um registro (dar um update)
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _repo.GetAlunoById(id, false);
            if (alu == null) return BadRequest("Aluno não encontrado");

            // Updates the database with the new Student row and saves the changes to the database
            _repo.Update(aluno);
            if(_repo.SaveChanges()) return Ok(aluno);

            return BadRequest("Aluno não atualizado");
        }

        // api/aluno/1
        // atualizar um registro parcialmente
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _repo.GetAlunoById(id, false);
            if (alu == null) return BadRequest("Aluno não encontrado");

            _repo.Update(aluno);
            if(_repo.SaveChanges()) return Ok(aluno);

            return BadRequest("Aluno não atualizado");
        }

        // api/aluno
        // deletar um registro
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Finds the student to be removed by Id
            var alu = _repo.GetAlunoById(id, false);
            if (alu == null) return BadRequest("Aluno não encontrado.");

            // Deletes student from database and saves the changes
            _repo.Delete(alu);
            if(_repo.SaveChanges()) return Ok("Aluno deletado");

            return BadRequest("Aluno não atualizado");
        }
    }
}