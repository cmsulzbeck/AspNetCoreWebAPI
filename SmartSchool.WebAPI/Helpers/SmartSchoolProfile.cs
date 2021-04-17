using AutoMapper;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Helpers
{
    public class SmartSchoolProfile : Profile
    {
        public SmartSchoolProfile()
        {
            // AlunoDto é o destino mencionado abaixo, e Aluno.cs é src
            CreateMap<Aluno, AlunoDto>()
                .ForMember(
                    // Implementa a questão do nome ser igual a nome + sobrenome na classe AlunoDto
                    // destino
                    dest => dest.Nome,
                    opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
                )
                .ForMember(
                    dest => dest.Idade,
                    opt => opt.MapFrom(src => src.DataNascimento.GetCurrentAge())
                );

            CreateMap<AlunoDto, Aluno>();

            // Como o mapeamento de Aluno para AlunoRegistrarDto será exato, pode ocorrer apenas o ReverseMap, já que o intuito do
            // mapeamento é registrar no banco de dados via API
            CreateMap<Aluno, AlunoRegistrarDto>().ReverseMap();

            // Mapping Professor class to ProfessorDto and ProfessorRegisterDto
            // Professor is src and ProfessorDto is dest
            CreateMap<Professor, ProfessorDto>()
                // Formating first name and last name
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
                );

            CreateMap<ProfessorDto, Professor>();
            // Mapping update functions to Professor class
            CreateMap<Professor, ProfessorRegistrarDto>().ReverseMap();
        }
    }
}