using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;
using webapi.filmes.manha.Domains;
using webapi.filmes.manha.interfaces;
using webapi.filmes.manha.repositories;

namespace webapi.filmes.manha.controllers
{
    // define que a rota de um arequisiçao sera no seguinte formato
    // ex: http://localhost:5000/api/genero
    [Route("api/[controller]")]

    //define que e um controlador de api
    [ApiController]

    // define que o tipo de resposta da api sera no forma json
    [Produces("application/json")]
    [Authorize(Roles = "administrador,comum")]

    //Método controlador que herda da controller base 
    //onde será criado os Endpoints (rotas)
    public class GeneroController : ControllerBase
    {
        /// <summary>
        /// objeto generointerfaces que ira receber todos os métodos definidos na interface IGenero interface
        /// </summary>
        private IGeneroRepository _generoRepository { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GeneroController()
        {
            _generoRepository = new GeneroRepository();

        }
        [HttpGet]
       
        public ActionResult Get()
        {

            try
            {
                //cria uma lista que recebe os dados da requisiçao
                List<GeneroDomain> listaGeneros = _generoRepository.ListarTodos();

                //retorna a lista no ormato json com status code 200 ok
                return Ok(listaGeneros);
            }
            catch (Exception erro)
            {
                // retorna um status cod badrequest 400 e a mensagem de erro
                return BadRequest(erro.Message);
            }
        }





        /// <summary>
        /// end point que aciona o metodo de cadastro de genero
        /// </summary>
        /// <param name="NovoGenero">objeto recebido</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(GeneroDomain NovoGenero)
        {
            try
            {
                //fazendo uma chamada de metodo cadastrar passando o objeto
                _generoRepository.cadastrar(NovoGenero);
                //retorna o status code 201(created)
                return StatusCode(201);
            }
            catch (Exception erro)
            {
                //retorna um status code (400)erro
                return BadRequest(erro.Message);
            }

        }

        [HttpDelete]
        public IActionResult Deletar(int Id)
        {
            try
            {
                //fazendo uma chamada de metodo Deletar passando o objeto
                _generoRepository.deletar(Id);
                //retorna o status code 202(accepted)
                return StatusCode(202);
            }
            catch (Exception erro)
            {
                //retorna um status code (400)erro
                return BadRequest(erro.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Buscar(int id)
        {
            try
            {

                GeneroDomain GeneroBuscados = _generoRepository.BuscarPorId(id);
                if (GeneroBuscados == null)
                {
                    return NotFound("Nenhum genero foi encontrado");
                }

                //retorna o status code 204(no content)
                return Ok(GeneroBuscados);
            }
            catch (Exception erro)
            {
                //retorna um status code (400)erro
                return BadRequest(erro.Message);
            }
        }

        [HttpPut]
        public IActionResult AtualizarCorpo(GeneroDomain generoNovo)
        {
            try
            {
                if (generoNovo == null)
                {
                    return NotFound("Nenhum genero foi Digitado");
                }
                _generoRepository.AtualizarIdCorpo(generoNovo);

                return StatusCode(204);
            }

            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }


        }

        [HttpPut("{id}")]
        public IActionResult AtualizarUrl(int id, GeneroDomain generoNovo)
        {
            try
            {
                if (generoNovo == null)
                {
                    return NotFound("Nenhum genero foi Digitado");
                }
                _generoRepository.AtualizarIdUrl(id, generoNovo);

                return StatusCode(204);
            }

            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }


        }
    }
}
