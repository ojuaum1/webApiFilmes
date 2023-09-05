using PrimeiroProjeto.Interfaces;
using System.Data.SqlClient;
using webapi.filmes.manha.Domains;

namespace webapi.filmes.manha.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private string StringConexao = "Data Source = note03-s15; Initial Catalog = Filmes; User Id = sa; pwd = Senai@134; TrustServerCertificate = true";
        public void AtualizarIdCorpo(FilmeDomain Filme)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string stringUpdate = "UPDATE Filme SET  Titulo = @filmeNome, IdGenero = @filmeGenero WHERE IdFilme = @idFilme";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(stringUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@filmeNome", Filme.Titulo);
                    cmd.Parameters.AddWithValue("@filmeGenero", Filme.IdGenero);
                    cmd.Parameters.AddWithValue("@idFilme", Filme.IdFilme);
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void AtualizarIdUrl(int id, FilmeDomain Filme)
        {
            using (SqlConnection con = new SqlConnection())
            {
                string stringUpdateUrl = "UPDATE Filme SET  Titulo = @filmeNome, IdGenero = @filmeGenero WHERE IdFilme = @idFilme";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(stringUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@filmeNome", Filme.Titulo);
                }
            }

        }

        public FilmeDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string stringBuscarId = "SELECT Genero.Nome, Filme.Titulo, Filme.IdFilme, Filme.IdGenero FROM Filme LEFT JOIN Genero ON Genero.IdGenero = Filme.IdGenero WHERE IdFilme = @idFilme";
                using (SqlCommand cmd = new SqlCommand(stringBuscarId, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@idFilme", id);

                    SqlDataReader rdr;

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        FilmeDomain filme = new FilmeDomain()
                        {
                            IdFilme = Convert.ToInt32(rdr["IdFilme"]),
                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),
                            Titulo = rdr["Titulo"].ToString(),
                            Genero = new GeneroDomain
                            {
                                Nome = rdr["Nome"].ToString(),
                                IdGenero = Convert.ToInt32(rdr["IdGenero"])
                            }
                        };
                        return filme;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public void Cadastrar(FilmeDomain novoFilme)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string stringPost = "INSERT INTO Filme(IdGenero,Titulo) VALUES(@idGenero,@filmeTitulo)";
                con.Open();

                using (SqlCommand cmd = new SqlCommand(stringPost, con))
                {
                    cmd.Parameters.AddWithValue("idGenero", novoFilme.IdGenero);
                    cmd.Parameters.AddWithValue("@filmeTitulo", novoFilme.Titulo);


                    cmd.ExecuteNonQuery();
                }
            }


        }

        public void Deletar(int IdFilme)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string stringDelete = "DELETE FROM Filme WHERE IdFilme = @idFilme";
                con.Open();
                using (SqlCommand cmd = new SqlCommand(stringDelete, con))
                {
                    cmd.Parameters.AddWithValue("@idFilme", IdFilme);


                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<FilmeDomain> ListarFilme()
        {
            List<FilmeDomain> listaFilme = new List<FilmeDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string stringQuery = "SELECT IdFilme, Filme.IdGenero, Genero.Nome, Titulo FROM Filme LEFT JOIN Genero ON Filme.IdGenero = Genero.IdGenero";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(stringQuery, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        FilmeDomain filme = new FilmeDomain()
                        {
                            Titulo = rdr["Titulo"].ToString(),
                            IdFilme = Convert.ToInt32(rdr["IdFilme"]),
                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),
                            Genero = new GeneroDomain()
                            {
                                Nome = rdr["Nome"].ToString(),
                                IdGenero = Convert.ToInt32(rdr["IdGenero"])
                            }
                        };

                        listaFilme.Add(filme);
                    }
                }
            }
            return listaFilme;

        }
    }
}
