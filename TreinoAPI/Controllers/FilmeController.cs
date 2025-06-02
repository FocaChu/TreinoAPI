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
            new FilmeModel { Id = 1, Titulo = "O Senhor dos Anéis", Genero = Genero.Fantasia},
            new FilmeModel { Id = 2, Titulo = "Matrix", Genero = Genero.FiccaoCientifica },
            new FilmeModel { Id = 3, Titulo = "Interestelar", Genero = Genero.FiccaoCientifica },
            new FilmeModel { Id = 4, Titulo = "A Origem", Genero = Genero.FiccaoCientifica },
            new FilmeModel { Id = 5, Titulo = "O Poderoso Chefão", Genero = Genero.Aventura}
        };

        [HttpGet]
        public ActionResult<List<FilmeModel>> GetFilmes()
        {
            if (filmes == null || filmes.Count == 0)
            {
                return NotFound("Nenhum filme encontrado.");
            }

            return Ok(filmes);
        }

        [HttpGet("{id}")]
        public ActionResult<FilmeModel> GetFilmeById(int id)
        {
            FilmeModel filme = filmes.Find(f => f.Id == id);

            if (filme != null)
            {
                return Ok(filme);
            }

            return NotFound($"Nenhum filme com id {id} encontrado.");
        }

        [HttpGet("genero/{genero}")]
        public ActionResult<List<FilmeModel>> GetFilmesByGenero(Genero genero)
        {
            List<FilmeModel> filmesPorGenero = filmes.Where(f => f.Genero == genero).ToList();
            if (filmesPorGenero == null || filmesPorGenero.Count == 0)
            {
                return NotFound($"Nenhum filme encontrado com o gênero {genero}.");
            }
            return Ok(filmesPorGenero);
        }

        [HttpPost]
        public ActionResult<FilmeModel> PostFilme([FromBody] FilmeModel filme)
        {
            if (filme == null)
            {
                return BadRequest("Filme não pode ser nulo.");
            }

            filme.Id = filmes.Count > 0 ? filmes.Max(f => f.Id) + 1 : 1;

            filmes.Add(filme);
            return CreatedAtAction(nameof(GetFilmeById), new { id = filme.Id }, filme);
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

            filmeAntigo.Titulo = filme.Titulo;
            return Ok(filmeAntigo);
        }

        [HttpDelete("{id}")]
        public ActionResult<FilmeModel> DeleteFilme(int id)
        {
            FilmeModel filme = filmes.Find(f => f.Id == id);

            if (filme == null)
            {
                return NotFound($"Nenhum filme com id {id} encontrado.");
            }

            filmes.Remove(filme);
            return Ok(filme);
        }
    }
}
