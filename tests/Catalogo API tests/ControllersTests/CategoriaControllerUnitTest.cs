using Catalogo_API.Context;
using Catalogo_API.Controllers;
using Catalogo_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo_API_tests.ControllersTests
{
    public class CategoriaControllerUnitTest
    {
        private readonly CategoriaController _controller;
        private readonly AppDbContext _virtualContext;

        public CategoriaControllerUnitTest()
        {
            //Criação do banco de dados imaginário para os testes
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _virtualContext = new AppDbContext(options);

            _controller = new CategoriaController(_virtualContext);

            //ATENÇÃO: Esse banco imaginário NÃO é PERSISTENTE durante toda a execussão dos testes.
            //Testes individuais tem seus próprios bancos.
        }

        private async Task PopularBanco()
        {
            //Adiciona categorias
            _virtualContext.AddRange(
            new Categoria
            {
                CategoriaId = 1,
                NomeCategoria = "Bebidas",
                ImgUrl = ""
            },
            new Categoria
            {
                CategoriaId = 2,
                NomeCategoria = "Comidas",
                ImgUrl = ""
            },
            new Categoria
            {
                CategoriaId = 3,
                NomeCategoria = "Brinquedos",
                ImgUrl = ""
            },
            new Categoria
            {
                CategoriaId = 4,
                NomeCategoria = "Eletrônicos",
                ImgUrl = ""
            });

            //Adiciona produtos para as categorias feitas
            _virtualContext.AddRange(
            new Produto
            {
                ProdutoId = 1,
                NomeProduto = "Wisky saBOR energético",
                DescricaoProduto = "não é com energético, é saBOOORR energérico",
                Preco = 50.90m,
                ImagemUrl = "",
                Estoque = 5,
                DataCadastro = DateTime.Today,
                CategoriaId = 1
            },
            new Produto
            {
                ProdutoId = 2,
                NomeProduto = "Pão de queijo",
                DescricaoProduto = "Um (uma unidade) de Pão, saBOR queijo",
                Preco = 99,
                ImagemUrl = "",
                Estoque = 12,
                DataCadastro = DateTime.Today,
                CategoriaId = 2
            },
            new Produto
            {
                ProdutoId = 3,
                NomeProduto = "PC gamer pichau promoção do gordão",
                DescricaoProduto = "PC com 4 orgivas de memoria ram, muito bom",
                Preco = 9999.99m,
                ImagemUrl = "",
                Estoque = 10,
                DataCadastro = DateTime.Today,
                CategoriaId = 4
            });

            await _virtualContext.SaveChangesAsync();

        }
        
        [Fact(DisplayName ="Teste de GET geral")]
        public async Task HttpGet_PegarTodosOsItens_RetornaOK()
        {
            //Arrange
            await PopularBanco();

            var esperado = StatusCodes.Status200OK;

            //Act
            var resultado = await _controller.Get();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            
            Assert.Equal(esperado, okResult.StatusCode);
        }

        [Fact(DisplayName = "Teste de GET por ID")]
        public async Task HttpGet_PegarPorId_RetornaOK()
        {
            //Arrange
            await PopularBanco();

            var esperado = StatusCodes.Status200OK;

            //Act
            var resultado = await _controller.Get(2);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);

            Assert.Equal(esperado, okResult.StatusCode);
        }

        [Fact(DisplayName = "Teste de GET listando categorias e produtos")]
        public async Task HttpGetCategoriaProdutos_ListarCategoriasEProdutos_RetornaOK()
        {
            //Arrange
            await PopularBanco();

            var esperado = StatusCodes.Status200OK;

            //Act
            var resultado = await _controller.GetCategoriaProdutos();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);

            Assert.Equal(esperado, okResult.StatusCode);
        }

        [Fact(DisplayName = "Teste de POST")]
        public async Task HttpPost_PostarCategoria_RetornaCreated()
        {
            //Arrange
            await PopularBanco();
            var esperado = StatusCodes.Status201Created;

            //Act
            var resultado = await _controller.Post(
                new Categoria
                {
                    NomeCategoria = "Categoria de teste",
                    ImgUrl = ""
                });

            //Assert
            var createdResult = Assert.IsType<CreatedAtRouteResult>(resultado);

            Assert.Equal(esperado, createdResult.StatusCode);
        }

        [Fact(DisplayName = "Teste de PUT")]
        public async Task HttpPut_AtualizarCategoria_RetornaOK()
        {
            //Arrange
            await PopularBanco();

            _virtualContext.ChangeTracker.Clear();

            var esperado = StatusCodes.Status200OK;

            //Act
            var resultado = await _controller.Put(
                1,
                new Categoria
                {
                    CategoriaId = 1,
                    NomeCategoria = "Teste do PUT",
                    ImgUrl = "modificação"
                });

            //Assert
            var putResult = Assert.IsType<OkObjectResult>(resultado);

            Assert.Equal(esperado, putResult.StatusCode);
        }

        [Fact(DisplayName = "Teste de DELETE")]
        public async Task HttpDelete_DeletarCategoria_RetornaOK()
        {
            //Arrange
            await PopularBanco();
            var esperado = StatusCodes.Status200OK;

            //Act
            var resultado = await _controller.Delete(2);

            //Assert
            var putResult = Assert.IsType<OkObjectResult>(resultado);

            Assert.Equal(esperado, putResult.StatusCode);
        }
    }
}
