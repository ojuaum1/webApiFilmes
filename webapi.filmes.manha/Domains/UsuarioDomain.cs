using System.ComponentModel.DataAnnotations;

namespace webapi.filmes.manha.Domains
{
    public class UsuarioDomain
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "O campo email e obrigatorio!")]
        public string Email { get; set; }

        [StringLength(20,MinimumLength =3, ErrorMessage ="A senha deve ter no minimo 3 caracteres e no maximo 29")]
        [Required(ErrorMessage ="o campo senha e obrigatorio")]
        public string Senha { get; set; }
        public string Permissao { get; set; }

    }
}
