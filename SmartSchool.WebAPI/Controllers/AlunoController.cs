using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebAPI.Helpers;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        /// <summary>
        /// Dependency injection on IRepository and IMapper
        /// </summary>
        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for AlunoController
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public AlunoController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        // api/aluno
        /// <summary>
        /// Method for returning all Students
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            var alunos = await _repo.GetAllAlunosAsync(pageParams, true);

            var alunosResult = _mapper.Map<IEnumerable<AlunoDto>>(alunos);

            Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPages);

            return Ok(alunosResult);
        }

        /// <summary>
        /// Method for returning all StudentsDTO
        /// </summary>
        /// <returns></returns>
        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new AlunoRegistrarDto());
        }

        /// <summary>
        /// Method for returning a single Student based off provided ID
        /// </summary>
        /// <param name="id">ID do Aluno</param>
        /// <returns></returns>
        // api/aluno/id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado.");

            var alunoDto = _mapper.Map<AlunoDto>(aluno);

            return Ok(alunoDto);
        }

        // api/aluno
        /// <summary>
        /// Method for adding a Student to DataBase
        /// </summary>
        /// <param name="model">Objeto da classe DTO para inclusão no banco de dados</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            // Recebe o modelo do AlunoDto e mapeia para Aluno
            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);
            if (_repo.SaveChanges()) return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));

            return BadRequest("Aluno não cadastrado");
        }

        // api/aluno/1
        // utilizado para atualizar um registro (dar um update)
        /// <summary>
        /// Method for updating a Student in the DataBase
        /// </summary>
        /// <param name="id">ID do Aluno a ser atualizado</param>
        /// <param name="model">Objeto da classe AlunoDTO para atualização</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);

            // Updates the database with the new Student row and saves the changes to the database
            _repo.Update(aluno);
            if (_repo.SaveChanges()) return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));

            return BadRequest("Aluno não atualizado");
        }

        // api/aluno/1
        // atualizar um registro parcialmente
        /// <summary>
        /// Method for updating partially a Student in the DataBase
        /// </summary>
        /// <param name="id">ID do Aluno a ser alterado parcialmente</param>
        /// <param name="model">Objeto da classe AlunoDTO para atualização de banco de dados</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);
            if (_repo.SaveChanges()) return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));

            return BadRequest("Aluno não atualizado");
        }

        // api/aluno
        // deletar um registro
        /// <summary>
        /// Method for deleting a student from DataBase
        /// </summary>
        /// <param name="id">ID do aluno a ser deletado</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Finds the student to be removed by Id
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("Aluno não encontrado.");

            // Deletes student from database and saves the changes
            _repo.Delete(aluno);
            if (_repo.SaveChanges()) return Ok("Aluno deletado");

            return BadRequest("Aluno não atualizado");
        }
    }
}