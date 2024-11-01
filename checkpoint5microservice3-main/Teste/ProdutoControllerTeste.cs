using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_app_domain;
using web_app_performance.Controllers;
using web_app_performance.Model;
using web_app_repository;

namespace Teste
{
    public class ProdutoControllerTeste
    {
        private readonly Mock<IProdutoRepository> _userRepositoryMock;
        private readonly ProdutoController _controller;

        public ProdutoControllerTeste()
        {
            _userRepositoryMock = new Mock<IProdutoRepository>();
            _controller = new ProdutoController(_userRepositoryMock.Object);

        }
        [Fact]
        public async Task Get_ListarProdutosOk()
        {
            //arrange
            var produtos = new List<Produto>()
            {
                new Produto()
                {
                    Id = 1,
                    Nome = "Arroz",
                    Preco = 3.50,
                    QuantidadeEstoque = 20,
                    DataCriacao = "14/10/2024"

                }
            };

            _userRepositoryMock.Setup(r => r.ListarProduto()).ReturnsAsync(produtos);

            //Act
            var result = await _controller.GetProduto();

            //Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(JsonConvert.SerializeObject(produtos), JsonConvert.SerializeObject(okResult.Value));
        }

        [Fact]
        public async Task Get_ListarRetornarNotFound()
        {
            _userRepositoryMock.Setup(u => u.ListarProduto()).ReturnsAsync((IEnumerable<Produto>)null);

            var result = await _controller.GetProduto();

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_SalvarProduto()
        {
            //arrange
            var produto = new Produto()
            {
                Id = 1,
                Nome = "Arroz",
                Preco = 4.50,
                QuantidadeEstoque = 20,
                DataCriacao = "14/10/2024"
            };
            _userRepositoryMock.Setup(u => u.SalvarProduto(It.IsAny<Produto>())).Returns(Task.CompletedTask);

            //Act
            var result = await _controller.Post(produto);
            Assert.IsType<OkObjectResult>(result);
            _userRepositoryMock.Verify(u => u.SalvarProduto(It.IsAny<Produto>()), Times.Once);
        }
    }
}
