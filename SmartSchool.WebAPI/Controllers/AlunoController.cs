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
        private readonly SmartContext _context;

        public AlunoController(SmartContext context) { 
            _context = context;
        }

        // api/aluno
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Alunos);
        }

        // api/aluno/byId/3
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);

            if (aluno == null) return BadRequest("O Aluno não foi encontrado.");

            return Ok(aluno);
        }

        // api/aluno/nome
        [HttpGet("ByName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome));

            if (aluno == null) return BadRequest("O Aluno não foi encontrado.");

            return Ok(aluno);
        }

        // api/aluno
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            // Adds the database with the new Student row and saves the changes to the database
            _context.Add(aluno);
            _context.SaveChanges();

            return Ok(aluno);
        }

        // api/aluno/1
        // utilizado para atualizar um registro (dar um update)
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(alu == null) return BadRequest("Aluno não encontrado");

            // Updates the database with the new Student row and saves the changes to the database
            _context.Update(aluno);
            _context.SaveChanges();

            return Ok(aluno);
        }

        // api/aluno/1
        // atualizar um registro parcialmente
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(alu == null) return BadRequest("Aluno não encontrado");

            _context.Update(aluno);
            _context.SaveChanges();

            return Ok(aluno);
        }

        // api/aluno
        // deletar um registro
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Finds the student to be removed by Id
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if(aluno == null) return BadRequest("Aluno não encontrado.");

            // Deletes student from database and saves the changes
            _context.Remove(aluno);
            _context.SaveChanges();

            return Ok();
        }
    }
}