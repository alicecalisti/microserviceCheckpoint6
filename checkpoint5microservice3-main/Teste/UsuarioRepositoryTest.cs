using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_app_performance.Model;
using web_app_repository;

namespace Teste
{
    public class UsuarioRepositoryTest
    {
        [Fact]
        public async Task ListaUsuarios()
        {
            var usuarios = new List<Usuario>()
            {
                new Usuario()
                {
                    Email = "alice@email.com",
                    Id = 1,
                    Nome = "Alice Calisti"
                },
                 new Usuario()
                {
                    Email = "calisti@email.com",
                    Id = 2,
                    Nome = "Daniel Calisti"
                }
            };

            var userRepositoryMock = new Mock<IUsuarioRepository>();
            userRepositoryMock.Setup(u => u.ListarUsuarios()).ReturnsAsync(usuarios);
            var userRepository = userRepositoryMock.Object;

            //act
            var result = await userRepository.ListarUsuarios();

            //Assert
            Assert.Equal(usuarios, result);
        }

        [Fact]
        public async Task SalvarUsuario()
        {
            //Arrange
            var usuario = new Usuario()
            {
                Id = 1,
                Email = "souza@email.com",
                Nome = "Alice Calisti"
            };

            var userReposirotyMock = new Mock<IUsuarioRepository>();
            userReposirotyMock.Setup(u => u.SalvarUsuario(It.IsAny<Usuario>())).Returns(Task.CompletedTask);
            var userRepository = userReposirotyMock.Object;

            //act
            await userRepository.SalvarUsuario(usuario);

            //assert
            userReposirotyMock.Verify(u => u.SalvarUsuario(It.IsAny<Usuario>()), Times.Once);
        }
    }
}
