namespace Catalogo_API.Filters
{
    public class ProdutoFilter
    {
        public string? Nome {  get; set; }
        public decimal? PrecoMin { get; set; }
        public decimal? PrecoMax { get; set; }
    }
}
