using Catalogo_API.Context;
using Catalogo_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        //Get comum
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            try
            {
                var produtos = await _context.Produtos.AsNoTracking().ToListAsync();

                if (!produtos.Any())
                    return NotFound("Produtos não encontrados!");

                return Ok(produtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Ocorreu um erro ao processar a requisição, por favor tente novamente mais tarde.");
            }
        }

        //Get filtrado pelo ID do item
        [HttpGet("{id:int:min(1)}", Name ="ObterProduto")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            try
            {
                var produtos = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.ProdutoId == id);

                if (produtos == null)
                    return NotFound("Produto não existe!");

                return Ok(produtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Ocorreu um erro ao processar a requisição, por favor tente novamente mais tarde.");
            }
        }

        //Post comum
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Produto produto)
        {
            if (produto == null)
                return BadRequest("Erro ao cadastrar item: Requisição não possue corpo!");

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("ObterProduto",
                                             new { id = produto.ProdutoId },
                                             produto);
        }

        //Put comum
        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
                return BadRequest($"Erro ao atualizar item: ID do item diverge. \nID informado: {id} \nID dado na requisição: {produto.ProdutoId}");

            var existe = await _context.Produtos.AnyAsync(c => c.ProdutoId == id);

            if (!existe)
                return NotFound("Não foi possível encontrar essa requisição. O ID informado não obteve éxito na pesquisa do item");

            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(produto);
        }

        //Hard Delete comum
        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Delete(int id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);

            if (produto == null)
                return NotFound($"Produto de ID {id} não foi encontrado ou já foi deletado.");

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return Ok(produto);
        }
    }
}
