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
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categoria = _context.Categorias.ToList();

            if (categoria == null)
                return NotFound("Categorias não encontradas!");

            return categoria;
        }

        //Get filtrado pelo ID do item
        [HttpGet("{id:int}", Name = "ObterCategorias")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

            if (categoria == null)
                return NotFound("Categoria não existe!");

            return categoria;
        }

        //Get listando Categorias junto dos produtos
        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriaProdutos()
        {
            return _context.Categorias.Include(p => p.Produtos).ToList();
        }

        //Post comum
        [HttpPost]
        public ActionResult Post([FromBody] Categoria categoria)
        {
            if (categoria == null)
                return BadRequest();

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategorias",
                                             new { id = categoria.CategoriaId },
                                             categoria);
        }

        //Put comum
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
                return BadRequest();

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }

        //Hard Delete comum
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

            if (categoria == null)
                return NotFound("Produto não encontrado.");

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }

    }
}
