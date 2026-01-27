using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogo_API.Migrations
{
    public partial class PopulaProdutos : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO produtos(NomeProduto,DescricaoProduto,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId)" +
                    "VALUES ('Caco Coca','Refrigerante de Cola 2L', 5.45,'cacocola.jpg',50,now(),1)");

            mb.Sql("INSERT INTO produtos(NomeProduto,DescricaoProduto,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId)" +
                    "VALUES ('X-burger','Hamburger muito gostoso mano confia', 10.90,'xburguer.jpg',20,now(),2)");

            mb.Sql("INSERT INTO produtos(NomeProduto,DescricaoProduto,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId)" +
                    "VALUES ('Chocolate','Chocolate comum, não compre', 999.99,'chocolate.jpg',999,now(),3)");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Produtos");
        }
    }
}