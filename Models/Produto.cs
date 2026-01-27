namespace Catalogo_API.Models;

public class Produto
{
    public int ProdutoId { get; set; }
    public string? NomeProduto { get; set; }
    public string? DescricaoProduto { get; set; }
    public decimal Preco { get; set; }
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
}
