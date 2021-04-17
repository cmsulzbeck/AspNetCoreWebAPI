using System;

namespace SmartSchool.WebAPI.Dtos
{
    public class AlunoDto
    {
        /// <summary>
        /// Indentificador e chave do banco
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Chave do Aluno, para outros negócios na instituição
        /// </summary>
        public int Matricula { get; set; }
        /// <summary>
        /// Nome e Sobrenome do Aluno
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Telefone do Aluno
        /// </summary>
        public string Telefone { get; set; }
        /// <summary>
        /// Idade do Aluno
        /// </summary>
        public int Idade { get; set; }
        /// <summary>
        /// Data de Inscrição do Aluno
        /// </summary>
        public DateTime DataIni { get; set; }
        /// <summary>
        /// Situação do aluno (Ativo/Inativo)
        /// </summary>
        public bool Ativo { get; set; }
    }
}