using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogo_API.Migrations
{
    public partial class PopulaCategorias : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("insert into Categorias(NomeCategoria, ImgUrl) values ('Bebidas', 'bebidas.jpg')");
            mb.Sql("insert into Categorias(NomeCategoria, ImgUrl) values ('Lanches', 'lanches.jpg')");
            mb.Sql("insert into Categorias(NomeCategoria, ImgUrl) values ('Sobremesas', 'sobremesas.jpg')");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categorias");
        }
    }
}
