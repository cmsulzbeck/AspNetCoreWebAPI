using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public ProfessorController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        // Métodos são:
        // Get (todos os professores): OK
        // api/professores
        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repo.GetAllProfessores(true);

            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professores));
        }

        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new ProfessorRegistrarDto());
        }

        // GetById (Um único professor baseado em Id): OK
        // api/professores/id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorById(id, false);
            if (professor == null) return BadRequest("Professor não encontrado.");

            return Ok(_mapper.Map<ProfessorDto>(professor));
        }

        // Post: OK
        //api/professor
        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var professor = _mapper.Map<Professor>(model);

            _repo.Add(professor);
            if (_repo.SaveChanges()) return Ok(professor);

            return BadRequest("Professor não cadastrado");
        }

        // Put (passando um Id como parâmetro): OK
        // api/professor/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {
            // Need to close after the object is found
            var professor = _repo.GetProfessorById(id, false);
            if (professor == null) return BadRequest("Professor não encontrado.");

            _mapper.Map(model, professor);

            _repo.Update(professor);
            if (_repo.SaveChanges()) return Ok(professor);

            return BadRequest("Professor não atualizado");
        }

        // Patch (passando um Id como parâmetro): OK
        // api/professor/id
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDto model)
        {
            var professor = _repo.GetProfessorById(id, false);
            if (professor == null) return BadRequest("Professor não encontrado.");

            _mapper.Map(model, professor);

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