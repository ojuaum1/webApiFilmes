using webapi.filmes.manha.Domains;

namespace webapi.filmes.manha.interfaces
{
    /// <summary>
    /// interface responsavel pelo repositorio generoRepository
    /// define os metodos que ser'ao implementados pelo repositorio
    /// </summary>
    public interface IGeneroRepository
    {
        //CRUD
        //TipodeRetorno nomemetodo(tipoparametro nomeparametro)

        /// <summary>
        /// cadastrar novo genero
        /// </summary>
        /// <param name="NovoGenero">objeto que será cadastrado</param>

        void cadastrar(GeneroDomain NovoGenero);
        
        /// <summary>
        /// Retornar todos os generos cadastrados 
        /// </summary>
        /// <returns> retorna a lista de objetos generos </returns>
         List<GeneroDomain> ListarTodos();

        /// <summary>
        /// buscar um objeto pelo id
        /// </summary>
        /// <param name="id">id do objeto que sera buscado</param>
        /// <returns>objeto que foi buscado</returns>
        GeneroDomain buscarpeloid(int id);


        /// <summary>
        /// atualiza um genero existente passando o id pelo corpo da requisiçao
        /// </summary>
        /// <param name="Genero">Objeto genero com as novas informações</param>
        void AtualizarIdCorpo(GeneroDomain Genero);



        /// <summary>
        /// atualizar um genero existente passando o id pela Url da Requesiçao
        /// </summary>
        /// <param name="id">id do objeto a ser atualizado</param>
        /// <param name="genero">objeto com as novas informaçoes</param>
        void AtualizarIdUrl(int id, GeneroDomain genero);




       /// <summary>
       /// deletar um genero existente
       /// </summary>
       /// <param name="id">id do objeto que sera deletado</param>
        void deletar (int id);
        GeneroDomain BuscarPorId(int id);
    }
}
