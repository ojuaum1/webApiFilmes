
using webapi.filmes.manha.Domains;

namespace PrimeiroProjeto.Interfaces
{
    public interface IFilmesRepository
    {
        void Cadastrar(FilmeDomain novoFilme);
        List<FilmeDomain> ListarFilme();

        void AtualizarIdCorpo(FilmeDomain Filme);

        void AtualizarIdUrl(int id, FilmeDomain Filme);

        void Deletar(int IdFilme);

        FilmeDomain BuscarPorId(int id);
    }
}