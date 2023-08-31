using System.ComponentModel.DataAnnotations;

namespace webapi.filmes.manha.Domains
{
    /// <summary>
    /// Clase que representa a entidade (tabela) genero
    /// </summary>
    public class GeneroDomain
    {
        public int IdGenero { get; set; }

        [Required(ErrorMessage ="o Nome do genero é obrigatorio")]
        public string? Nome { get; set; }
    }
}
