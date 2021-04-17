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
    /// <summary>
    /// Controller Class for Professor Entity
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for ProfessorController Class
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public ProfessorController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        // api/professores
        /// <summary>
        /// Method for returning all Professors
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repo.GetAllProfessores(true);

            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professores));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new ProfessorRegistrarDto());
        }

        // api/professores/id
        /// <summary>
        /// Method for returning a single Professor by ID
        /// </summary>
        /// <param name="id">ID for Professor to be searched</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorById(id, false);
            if (professor == null) return BadRequest("Professor não encontrado.");

            return Ok(_mapper.Map<ProfessorDto>(professor));
        }

        //api/professor
        /// <summary>
        /// Method for inserting a Professor in the DataBase
        /// </summary>
        /// <param name="model">Object for ProfessorRegistraDTO for updating DataBase</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var professor = _mapper.Map<Professor>(model);

            _repo.Add(professor);
            if (_repo.SaveChanges()) return Ok(professor);

            return BadRequest("Professor não cadastrado");
        }

        // api/professor/id
        /// <summary>
        /// Method for Updating a Professor registry in the DataBase
        /// </summary>
        /// <param name="id">ID of the professor to be changed</param>
        /// <param name="model">Object of ProfessorRegistrarDto to update DataBase</param>
        /// <returns></returns>
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

        // api/professor/id
        /// <summary>
        /// Method for updating Professor Registry in the database
        /// </summary>
        /// <param name="id">ID of the professor to be changed</param>
        /// <param name="model">Object of ProfessorRegistrarDto to update DataBase</param>
        /// <returns></returns>
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

        // api/professor/id
        /// <summary>
        /// Method for deleting Professor registry from DataBase
        /// </summary>
        /// <param name="id">ID of the Professor to be deleted</param>
        /// <returns></returns>
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