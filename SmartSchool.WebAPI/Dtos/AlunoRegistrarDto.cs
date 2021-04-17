using System;

namespace SmartSchool.WebAPI.Dtos
{
    /// <summary>
    /// Este � o DTO de Aluno para registrar e atualizar no banco de dados.
    /// </summary>
    public class AlunoRegistrarDto
    {
        /// <summary>
        /// Indentificador e chave do banco.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Chave do Aluno, para outros neg�cios na institui��o.
        /// </summary>
        public int Matricula { get; set; }
        /// <summary>
        /// Nome e Sobrenome do Aluno.
        /// </summary>
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataIni { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; } = null;
        public bool Ativo { get; set; } = true;
    }
}