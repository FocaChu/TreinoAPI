using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreinoAPI.Models;

namespace TreinoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        public static List<FilmeModel> filmes = new List<FilmeModel>
        {
            new FilmeModel { Id = 1, Titulo = "Filme 1"},
            new FilmeModel { Id = 2, Titulo = "Filme 2"}
        };

        [HttpGet]
        public ActionResult<List<FilmeModel>> GetFilmes()
        {
            if(filmes == null || filmes.Count == 0)
            {
                return NotFound("Nenhum filme encontrado.");
            }

            return Ok(filmes);
        }

        [HttpGet("{id}")]
        public ActionResult<FilmeModel>  GetFilmeById(int id)
        {
            FilmeModel filme = filmes.Find(f => f.Id == id);

            if (filme != null)
            {
                return Ok(filme);
            }
            else
            {
                return NotFound($"Nenhum filme com id {id} encontrado.");
            }
        }

        [HttpPost]
        public ActionResult<FilmeModel> PostFilme([FromBody]FilmeModel filme)
        {
            if (filme == null)
            {
                return BadRequest("Filme não pode ser nulo.");
            }
            filmes.Add(filme);
            return Ok("Filme criado com sucesso");
        }

        [HttpPut("{id}")]
        public ActionResult<FilmeModel> PutFilme(int id, [FromBody] FilmeModel filme)
        {
            if (filme == null)
            {
                return BadRequest("Filme não pode ser nulo.");
            }

            FilmeModel filmeAntigo = filmes.Find(f => f.Id == id);

            if (filmeAntigo == null)
            {
                return NotFound($"Nenhum filme com id {id} encontrado.");
            }

            filmeAntigo.Id = id; // Manter o ID original
            filmeAntigo.Titulo = filme.Titulo;
            return Ok("Filme atualizado com sucesso");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteFilme(int id)
        {
            FilmeModel filme = filmes.Find(f => f.Id == id);

            if (filme == null)
            {
                return NotFound($"Nenhum filme com id {id} encontrado.");
            }
            else
            {
                filmes.Remove(filme);
                return Ok("Filme deletado com sucesso");
            }

        }
    }
}
