using System.Collections.ObjectModel;

namespace Catalogo_API.Models;

public class Categoria
{

    public Categoria()
    {
        Produtos = new Collection<Produto>();
    }

    //Definições das categorias
    public int CategoriaId { get; set; } //PK
    public string? NomeCategoria { get; set; }
    public string? ImgUrl { get; set; }

    //Definições da relação Categoria - Produto
    public ICollection<Produto>? Produtos { get; set; }
}
