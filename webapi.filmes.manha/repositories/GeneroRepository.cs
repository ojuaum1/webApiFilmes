using System.Data.SqlClient;
using System.Reflection;
using webapi.filmes.manha.Domains;
using webapi.filmes.manha.interfaces;

namespace webapi.filmes.manha.repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        /// <summary>
        /// data source
        /// </summary>
        private string stringconexao = "Data Source = note03-s15; initial Catalog = Filmes; user id = sa; pwd = Senai@134   ";
        private object con;
        private object convert;
        private object cmd;

        //Integrated Security = true 
        public void AtualizarIdCorpo(GeneroDomain Genero)
        {
            using (SqlConnection con = new SqlConnection(stringconexao))
            {
                    //abrr a conexão com o banco 
                con.Open();
                // DeclarativeSecurityAction a query a ser executada
                string QueryUpdate = "Update Genero SET Nome=@Nome WHERE IdGenero = @IdGenero";

                using (SqlCommand cmd = new SqlCommand(QueryUpdate, con))
                {
                    //passa o valor do parametro @nome 
                    cmd.Parameters.AddWithValue("@IdGenero", Genero.IdGenero);
                    cmd.Parameters.AddWithValue("@Nome", Genero.Nome);
                    //abre a coneçao com o banco de dados
                 

                    cmd.ExecuteNonQuery();
                }

            
            }
        }

        public void AtualizarIdUrl(int id, GeneroDomain generoNovo)
        {
            using (SqlConnection con = new SqlConnection(stringconexao))
            {
                BuscarPorId(id);
                // DeclarativeSecurityAction a query a ser executada
                String QueryUpdate = "Update Genero SET Nome=@Nome WHERE IdGenero = @IdGenero";

                using (SqlCommand cmd = new SqlCommand(QueryUpdate, con))
                {
                    //passa o valor do parametro @nome 
                    cmd.Parameters.AddWithValue("@IdGenero",id);
                    cmd.Parameters.AddWithValue("@Nome", generoNovo.Nome);
                    //abre a coneçao com o banco de dados
                    con.Open();

                    cmd.ExecuteNonQuery();
                }

                //abrr a conexão com o banco 
                con.Open();
            }
        }

        public GeneroDomain buscarpeloid(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// cadatrar um novo genero
        /// </summary>
        /// <param name="NovoGenero">objeto com as informaçoes que serao cadastradas </param>
        /// <exception cref="NotImplementedException"></exception>
        public void cadastrar(GeneroDomain NovoGenero)
        {
            //DeclarativeSecurityAction a conexao como parametro
            using (SqlConnection con = new SqlConnection(stringconexao))
            {
                //declara a query que sera execultada
                string QueryInsert = "INSERT INTO Genero(Nome) VALUES (@Nome)";
                //declara o sqlcommand passando a query que ser'a execultada e a conexao com o bd
                using (SqlCommand cmd = new SqlCommand(QueryInsert, con))
                {

                    //passa o valor do parametro @nome 
                    cmd.Parameters.AddWithValue("@Nome", NovoGenero.Nome);
                    //abre a coneçao com o banco de dados
                    con.Open();

                    cmd.ExecuteNonQuery();
                }

            }

        }


        public void deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringconexao))
            {
                String Querydelete = "DELETE fROM Genero WHERE IdGenero = @IdDeletado";

                using (SqlCommand cmd = new SqlCommand(Querydelete, con))
                {
                    //passa o valor do parametro @nome 
                    cmd.Parameters.AddWithValue("@IdDeletado", id);
                    //abre a coneçao com o banco de dados
                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }



        public GeneroDomain BuscarPorId(int id)
        {
            //Declara a Conexão passando a sting de conexão como paratemetro
            using (SqlConnection con = new SqlConnection(stringconexao))
            {
                // DeclarativeSecurityAction a query a ser executada
                String QueryBuscar = "select IdGenero, Nome FROM Genero WHERE IdGenero = @IdGenero";

                SqlDataReader rdr;
                //abrr a conexão com o banco 
                con.Open();

                using (SqlCommand cmd = new SqlCommand(QueryBuscar, con))
                {

                    cmd.Parameters.AddWithValue("@IdGenero", id);
                    // executa a query e armazena os dados na rdr
                    rdr = cmd.ExecuteReader();
                    //abre a coneçao com o banco de dados



                    if (rdr.Read())
                    {
                        GeneroDomain GeneroBuscado = new GeneroDomain()
                        {

                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),
                            Nome = rdr["Nome"].ToString()

                        };
                        return GeneroBuscado;
                    }
                    return null;
                }
            }
        }


        /// <summary>
        /// Listar todos os objetos generos 
        /// </summary>
        /// <returns>Lista de objeto genero</returns>

        public List<GeneroDomain> ListarTodos()
        {
            // cria uma lista  de objeto do tipo Genero 
            List<GeneroDomain> ListaGeneros = new List<GeneroDomain>();

            // declara a sql conection passando a string de conexao como parametro
            using (SqlConnection con = new SqlConnection(stringconexao))
            {
                //declara a instruçao a ser execultada 
                string queryselectAll = "SELECT IdGenero, Nome FROM Genero";

                //abre a conexao com o banco de dados
                con.Open();

                //declara o sqldatareader para percorrer a tabela do banco de dados
                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(queryselectAll, con))
                {
                    //executa a query e armazena 
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        GeneroDomain genero = new GeneroDomain()
                        {
                            //atribui a propriedade IdGenero o valor recebido no rdr
                            IdGenero = Convert.ToInt32(rdr[0]),
                            //atribui a propriedade Nome o valor recebido no rdr 
                            Nome = rdr["Nome"].ToString()
                        };
                        ListaGeneros.Add(genero);
                    }
                }

            }
            return ListaGeneros;
        }

    }
}
