using System.ComponentModel.DataAnnotations;

namespace webapi.filmes.manha.Domains
{
    /// <summary>
    /// classe que representa a (entidade ) tabela filme
    /// </summary>
    public class FilmeDomain
    {
        public int IdFilme { get; set; }

        [Required(ErrorMessage ="o titulo do filme é obrigatorio")]
        public String Titulo { get; set; }
        public int IdGenero { get; set; }
       
        
        /// referencia para a classe genero
        public GeneroDomain? Genero { get; set; }
    }
}
