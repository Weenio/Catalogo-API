using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Catalogo_API.Models;
[Table("Produtos")]
public class Produto
{
    //Definições do produto
    [Key]
    public int ProdutoId { get; set; } //PK

    [Required]
    [StringLength(80)]
    public string? NomeProduto { get; set; }

    [Required]
    [StringLength(300)]
    public string? DescricaoProduto { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }

    public float Estoque { get; set; }

    public DateTime DataCadastro { get; set; }


    //Chave estrangeira para as categorias dos produtos
    public int CategoriaId { get; set; } //FK

    [JsonIgnore]
    public Categoria? Categoria { get; set; }
}
