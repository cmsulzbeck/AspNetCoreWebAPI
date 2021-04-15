using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly SmartContext _context;

        public ProfessorController(SmartContext context) { 
            _context = context;
        }

        // TODO: Implementar métodos da AlunoController.cs para a classe ProfessorController.cs
        // Métodos são:
        // Get (todos os professores): OK
        // api/professores
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Professores);
        }

        // GetById (Um único professor baseado em Id): OK
        // api/professores/byId/id
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _context.Professores.FirstOrDefault(p => p.Id == id);

            if(professor == null) return BadRequest("Professor não encontrado.");

            return Ok(professor);
        }

        // GetByName (Um único professor baseado em Nome): OK
        // api/professor/byName/name
        [HttpGet("byName/{name}")]
        public IActionResult GetByName(string name)
        {
            var professor = _context.Professores.FirstOrDefault(p => p.Nome.Contains(name));

            if(professor == null) return BadRequest("Professor não encontrado");

            return Ok(professor); 
        }

        // Post: OK
        //api/professor
        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _context.Add(professor);
            _context.SaveChanges();

            return Ok(professor);
        }

        // Put (passando um Id como parâmetro): OK
        // api/professor/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            // Need to close after the object is found
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if(prof == null) return BadRequest("Professor não encontrado.");

            _context.Update(professor);
            _context.SaveChanges();

            return Ok(professor);
        }

        // Patch (passando um Id como parâmetro): OK
        // api/professor/id
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if(prof == null) return BadRequest("Professor não encontrado.");

            _context.Update(professor);
            _context.SaveChanges();

            return Ok(professor);
        }

        // Delete (passando um Id como parâmetro): OK
        // api/professor/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
            if(professor == null) return BadRequest("Professor não encontrado");

            _context.Remove(professor);
            _context.SaveChanges();

            return Ok();
        }


    }
}