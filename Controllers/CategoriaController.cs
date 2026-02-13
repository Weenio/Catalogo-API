using Catalogo_API.Context;
using Catalogo_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriaController(AppDbContext context)
        {
            _context = context;
        }

        //Get comum
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get()
        {
            try
            {
                var categoria = await _context.Categorias.AsNoTracking().ToListAsync();

                if (!categoria.Any())
                    return NotFound("Categorias não encontradas!");

                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Ocorreu um erro ao processar a requisição, por favor tente novamente mais tarde.");
            }
        }

        //Get filtrado pelo ID do item
        [HttpGet("{id:int:min(1)}", Name = "ObterCategorias")]
        public async Task<ActionResult<Categoria>> Get(int id)
        {
            try
            {
                var categoria = await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(p => p.CategoriaId == id);

                if (categoria == null)
                    return NotFound("Categoria não existe!");

                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Ocorreu um erro ao processar a requisição, por favor tente novamente mais tarde.");
            }
        }

        //Get listando Categorias junto dos produtos
        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriaProdutos()
        {
            try
            {
                return await _context.Categorias.AsNoTracking().Include(p => p.Produtos).ToListAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Ocorreu um erro ao processar a requisição, por favor tente novamente mais tarde.");
            }
        }

        //Post comum
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Categoria categoria)
        {
            if (categoria == null)
                return BadRequest("Erro ao cadastrar item: Requisição não possue corpo!");

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("ObterCategorias",
                                             new { id = categoria.CategoriaId },
                                             categoria);
        }

        //Put comum
        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
                return BadRequest($"Erro ao atualizar item: ID do item diverge. \nID informado: {id} \nID dado na requisição: {categoria.CategoriaId}");

            var existe = await _context.Categorias.AnyAsync(c => c.CategoriaId == id);

            if (!existe)
                return NotFound("Não foi possível encontrar essa requisição. O ID informado não obteve éxito na pesquisa do item");

            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(categoria);
        }

        //Hard Delete comum
        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Delete(int id)
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(p => p.CategoriaId == id);

            if (categoria == null)
                return NotFound($"Categoria de ID {id} não foi encontrado ou já foi deletado.");

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return Ok(categoria);
        }

    }
}
