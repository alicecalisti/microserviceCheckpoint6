using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_app_domain;
using web_app_performance.Model;
using web_app_repository;

namespace Teste
{
    public class ProdutoRepositoryTeste
    {
        [Fact]
        public async Task ListaProdutos()
        {
            var produtos = new List<Produto>()
            {
                new Produto()
                {
                    Id = 1,
                    Nome = "Arroz",
                    Preco = 3.50,
                    QuantidadeEstoque = 20,
                    DataCriacao = "14/10/2024"
                },
                 new Produto()
                {
                    Id = 2,
                    Nome = "Macarrão",
                    Preco = 2.50,
                    QuantidadeEstoque = 30,
                    DataCriacao = "11/10/2024"
                },
            };

            var userRepositoryMock = new Mock<IProdutoRepository>();
            userRepositoryMock.Setup(u => u.ListarProduto()).ReturnsAsync(produtos);
            var userRepository = userRepositoryMock.Object;

            //act
            var result = await userRepository.ListarProduto();

            //Assert
            Assert.Equal(produtos, result);
        }

        [Fact]
        public async Task SalvarProduto()
        {
            //Arrange
            var produto = new Produto()
            {
                Id = 1,
                Nome = "Arroz",
                Preco = 3.50,
                QuantidadeEstoque = 20,
                DataCriacao = "14/10/2024"
            };

            var userReposirotyMock = new Mock<IProdutoRepository>();
            userReposirotyMock.Setup(u => u.SalvarProduto(It.IsAny<Produto>())).Returns(Task.CompletedTask);
            var userRepository = userReposirotyMock.Object;

            //act
            await userRepository.SalvarProduto(produto);

            //assert
            userReposirotyMock.Verify(u => u.SalvarProduto(It.IsAny<Produto>()), Times.Once);
        }
    }
}
