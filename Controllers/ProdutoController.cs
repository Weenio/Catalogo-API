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
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _context.Produtos.ToList();

            if (produtos == null)
                return NotFound("Produtos não encontrados!");

            return produtos;
        }

        //Get filtrado pelo ID do item
        [HttpGet("{id:int}", Name ="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produtos = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produtos == null)
                return NotFound("Produto não existe!");

            return produtos;
        }

        //Post comum
        [HttpPost]
        public ActionResult Post([FromBody] Produto produto)
        {
            if (produto == null)
                return BadRequest();

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto",
                                             new { id = produto.ProdutoId },
                                             produto);
        }

        //Put comum
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
                return BadRequest();

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }

        //Hard Delete comum
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produto == null)
                return NotFound("Produto não encontrado.");

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }
    }
}
