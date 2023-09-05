using System.Data.SqlClient;
using webapi.filmes.manha.Domains;
using webapi.filmes.manha.interfaces;

namespace webapi.filmes.manha.repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        string stringconexao = "Data Source = note03-s15; initial Catalog = Filmes; user id = sa; pwd = Senai@134";
        public UsuarioDomain Login(string email, string senha)
        {


            using (SqlConnection con = new SqlConnection(stringconexao))
            {
                string emailzinho = "SELECT IdUsuario, Email,Permissao from Usuario Where Email = @Email and Senha = @Senha";
                
                con.Open();

                SqlDataReader rdr;
                using (SqlCommand cmd = new SqlCommand(emailzinho,con))
                {
                    cmd.Parameters.AddWithValue("@Email" ,email);
                    cmd.Parameters.AddWithValue("@Senha" ,senha);

                    rdr= cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        UsuarioDomain Usuario = new UsuarioDomain
                        {
                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"]),

                            Email = rdr["Email"].ToString(),

                            Permissao = rdr["Permissao"].ToString()   
                        };
                        return Usuario;
                    }
                    return null;
                }
            }

        }
    }
}



