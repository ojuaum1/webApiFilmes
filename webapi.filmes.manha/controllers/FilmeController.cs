using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeiroProjeto.Interfaces;
using PrimeiroProjeto.Repositories;
using webapi.filmes.manha.Domains;

namespace PrimeiroProjeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class FilmeController : ControllerBase
    {
        private IFilmeRepository _filmeRepository { get; set; }

        public FilmeController()
        {
            _filmeRepository = new FilmeRepository();
        }
        /// <summary>
        /// Método de listagem dos Filmes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<FilmeDomain> listaFilme = _filmeRepository.ListarFilme();
                return Ok(listaFilme);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }

        }

        /// <summary>
        /// Método de Postagem de novos Filmes
        /// </summary>
        /// <param name="novofilme"></param>
        /// <returns></returns>
        /// <summary>
        /// Método para pesquisa de um filme específico
        /// </summary>
        /// <param name="idFilme"></param>
        /// <returns></returns>
        [HttpGet("{idFilme}")]
        public IActionResult GetWithId(int idFilme)
        {
            try
            {
                if (_filmeRepository.BuscarPorId(idFilme) == null)
                {
                    return NotFound("Objeto não encontrado!");
                }
                else
                {
                    return Ok(_filmeRepository.BuscarPorId(idFilme));
                }

            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Método de cadastro de novos filmes
        /// </summary>
        /// <param name="novofilme"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(FilmeDomain novofilme)
        {
            try
            {

                _filmeRepository.Cadastrar(novofilme);
                return StatusCode(201);

            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }

        }

        /// <summary>
        /// Método de Deleção de filmes da lista
        /// </summary>
        /// <param name="idFilme"></param>
        /// <returns></returns>
        [HttpDelete("{idFilme}")]
        public IActionResult Delete(int idFilme)
        {
            try
            {
                _filmeRepository.Deletar(idFilme);

                return StatusCode(204);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPut]
        public IActionResult PutBody(FilmeDomain novofilme)
        {
            try
            {
                _filmeRepository.AtualizarIdCorpo(novofilme);

                return StatusCode(204);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

    }
}