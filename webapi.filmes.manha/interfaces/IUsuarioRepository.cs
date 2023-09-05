using webapi.filmes.manha.Domains;

namespace webapi.filmes.manha.interfaces
{
    public interface IUsuarioRepository
    {
        UsuarioDomain Login(string email, string senha);
    }
}
