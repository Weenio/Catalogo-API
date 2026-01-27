namespace Catalogo_API.Models;

public class Produto
{
    //Definições do produto
    public int ProdutoId { get; set; } //PK
    public string? NomeProduto { get; set; }
    public string? DescricaoProduto { get; set; }
    public decimal Preco { get; set; }
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }

    //Chave estrangeira para as categorias dos produtos
    public int CategoriaId { get; set; } //FK
    public Categoria? Categoria { get; set; }
}
