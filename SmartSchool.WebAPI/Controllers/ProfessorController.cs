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
        private readonly IRepository _repo;

        public ProfessorController(IRepository repo)
        {
            _repo = repo;
        }

        // Métodos são:
        // Get (todos os professores): OK
        // api/professores
        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllProfessores(true);
            return Ok(result);
        }

        // GetById (Um único professor baseado em Id): OK
        // api/professores/id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorById(id, false);

            if (professor == null) return BadRequest("Professor não encontrado.");

            return Ok(professor);
        }

        // Post: OK
        //api/professor
        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _repo.Add(professor);
            if (_repo.SaveChanges()) return Ok(professor);

            return BadRequest("Professor não cadastrado");
        }

        // Put (passando um Id como parâmetro): OK
        // api/professor/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            // Need to close after the object is found
            var prof = _repo.GetProfessorById(id, false);
            if (prof == null) return BadRequest("Professor não encontrado.");

            _repo.Update(professor);
            if (_repo.SaveChanges()) return Ok(professor);

            return BadRequest("Professor não atualizado");
        }

        // Patch (passando um Id como parâmetro): OK
        // api/professor/id
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = _repo.GetProfessorById(id, false);
            if (prof == null) return BadRequest("Professor não encontrado.");

           _repo.Update(professor);
            if (_repo.SaveChanges()) return Ok(professor);

            return BadRequest("Professor não atualizado");
        }

        // Delete (passando um Id como parâmetro): OK
        // api/professor/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetProfessorById(id, false);
            if (professor == null) return BadRequest("Professor não encontrado");

            _repo.Delete(professor);
            if (_repo.SaveChanges()) return Ok("Professor Deletado");

            return BadRequest("Professor não atualizado");
        }
    }
}