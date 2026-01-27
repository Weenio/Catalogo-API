using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalogo_API.Models;

[Table("Categorias")]
public class Categoria
{

    public Categoria()
    {
        Produtos = new Collection<Produto>();
    }

    //Definições das categorias
    [Key]
    public int CategoriaId { get; set; } //PK

    [Required]
    [StringLength(80)]
    public string? NomeCategoria { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImgUrl { get; set; }


    //Definições da relação Categoria - Produto
    public ICollection<Produto>? Produtos { get; set; }
}
