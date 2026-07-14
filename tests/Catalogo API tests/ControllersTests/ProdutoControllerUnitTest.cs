using Catalogo_API.Context;
using Catalogo_API.Controllers;
using Catalogo_API.Models;
using Catalogo_API.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo_API_tests.ControllersTests
{
    public class ProdutoControllerUnitTest
    {
        private readonly ProdutoController _controller;
        private readonly AppDbContext _virtualContext;

        //pequena alteração para teste de pipeline

        public ProdutoControllerUnitTest()
        {
            //Criação do banco de dados imaginário para os testes
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _virtualContext = new AppDbContext(options);

            _controller = new ProdutoController(_virtualContext);

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
        
        [Fact(DisplayName ="Teste de GET geral (Sem filtro)")]
        public async Task HttpGet_PegarTodosOsItensSemFiltro_RetornaOK()
        {
            //Arrange
            await PopularBanco();

            var esperado = StatusCodes.Status200OK;

            //Act
            var resultado = await _controller.Get(
                new ProdutoFilter
                {
                    Nome = null,
                    PrecoMin = null,
                    PrecoMax = null,
                });

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);
            
            Assert.Equal(esperado, okResult.StatusCode);
        }

        [Fact(DisplayName = "Teste de GET geral (Com preço entre 50 e 100)")]
        public async Task HttpGet_PegarTodosOsItensFiltroDePreco_RetornaOK()
        {
            //Arrange
            await PopularBanco();

            var esperado = StatusCodes.Status200OK;

            //Act
            var resultado = await _controller.Get(
                new ProdutoFilter
                {
                    Nome = null,
                    PrecoMin = 50,
                    PrecoMax = 100,
                });

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado.Result);

            Assert.Equal(esperado, okResult.StatusCode);
        }

        [Fact(DisplayName = "Teste de GET geral (Com nome = 'PC gamer pichau promoção do gordão')")]
        public async Task HttpGet_PegarTodosOsItensFiltroDeNome_RetornaOK()
        {
            //Arrange
            await PopularBanco();

            var esperado = StatusCodes.Status200OK;

            //Act
            var resultado = await _controller.Get(
                new ProdutoFilter
                {
                    Nome = "PC gamer pichau promoção do gordão",
                    PrecoMin = null,
                    PrecoMax = null,
                });

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

        [Fact(DisplayName = "Teste de POST")]
        public async Task HttpPost_PostarCategoria_RetornaCreated()
        {
            //Arrange
            await PopularBanco();
            var esperado = StatusCodes.Status201Created;

            //Act
            var resultado = await _controller.Post(
                new Produto
                {
                    NomeProduto = "Produto de Teste",
                    DescricaoProduto = "Um produto de teste genérico",
                    Preco = 1.1m,
                    ImagemUrl = "",
                    Estoque = 1,
                    DataCadastro = DateTime.Today,
                    CategoriaId = 2
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
                new Produto
                {
                    ProdutoId = 1,
                    NomeProduto = "Modificação de Teste",
                    DescricaoProduto = "Uma Modificação de Teste generica",
                    Preco = 1.1m,
                    ImagemUrl = "",
                    Estoque = 1,
                    DataCadastro = DateTime.Today,
                    CategoriaId = 2
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
