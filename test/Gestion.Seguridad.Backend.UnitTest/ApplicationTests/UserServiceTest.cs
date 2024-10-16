using AutoMapper;
using Gestion.Seguridad.Backend.Application.Services;
using Gestion.Seguridad.Backend.Domain.DTOs.Usuario;
using Gestion.Seguridad.Backend.Domain.Entities;
using Gestion.Seguridad.Backend.Infrastructure.Persistence.Repositories.Interfaces;
using Moq;

namespace Gestion.Seguridad.Backend.UnitTest.ApplicationTests
{
    public class UserServiceTest
    {
        [Fact]
        public async Task ListarAsync_ReturnsListOfUsuarios()
        {
            // Arrange
            var expectedList = new List<Usuario> { new Usuario() };
            var expectedDtoList = new List<ListarUsuarioDto> { new ListarUsuarioDto() };

            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IUsuarioRepository>();

            mockRepository.Setup(r => r.ListarAsync()).ReturnsAsync(expectedList);
            mockMapper.Setup(m => m.Map<IList<ListarUsuarioDto>>(expectedList)).Returns(expectedDtoList);

            var service = new UsuarioService(mockMapper.Object, mockRepository.Object);

            // Act
            var result = await service.ListarAsync();

            // Assert
            Assert.Equal(expectedDtoList.Count, result.Count);
            mockRepository.Verify(r => r.ListarAsync(), Times.Once);
        }

        [Fact]
        public async Task CrearAsync_ValidDto_ReturnsNewId()
        {
            // Arrange
            var expectedId = 1;
            var crearUsuarioDto = new CrearUsuarioDto { Clave = "password123" };
            var usuario = new Usuario { Clave = "hashedPassword123" };

            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IUsuarioRepository>();

            mockMapper.Setup(m => m.Map<Usuario>(crearUsuarioDto)).Returns(usuario);
            mockRepository.Setup(r => r.CrearAsync(usuario)).ReturnsAsync(expectedId);

            var service = new UsuarioService(mockMapper.Object, mockRepository.Object);

            // Act
            var result = await service.CrearAsync(crearUsuarioDto);

            // Assert
            Assert.Equal(expectedId, result);
            mockRepository.Verify(r => r.CrearAsync(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact]
        public async Task EditarAsync_ValidDto_ReturnsTrue()
        {
            // Arrange
            var editarUsuarioDto = new EditarUsuarioDto();
            var usuario = new Usuario();

            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IUsuarioRepository>();

            mockMapper.Setup(m => m.Map<Usuario>(editarUsuarioDto)).Returns(usuario);
            mockRepository.Setup(r => r.EditarAsync(usuario)).ReturnsAsync(true);

            var service = new UsuarioService(mockMapper.Object, mockRepository.Object);

            // Act
            var result = await service.EditarAsync(editarUsuarioDto);

            // Assert
            Assert.True(result);
            mockRepository.Verify(r => r.EditarAsync(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact]
        public async Task EliminarAsync_ValidId_ReturnsTrue()
        {
            // Arrange
            var id = 1;

            var mockRepository = new Mock<IUsuarioRepository>();
            mockRepository.Setup(r => r.EliminarAsync(id)).ReturnsAsync(true);

            var mockMapper = new Mock<IMapper>();
            var service = new UsuarioService(mockMapper.Object, mockRepository.Object);

            // Act
            var result = await service.EliminarAsync(id);

            // Assert
            Assert.True(result);
            mockRepository.Verify(r => r.EliminarAsync(id), Times.Once);
        }

        [Fact]
        public async Task ObtenerAsync_ValidId_ReturnsUsuarioDto()
        {
            // Arrange
            var id = 1;
            var usuario = new Usuario();
            var expectedDto = new ObtenerUsuarioDto();

            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IUsuarioRepository>();

            mockRepository.Setup(r => r.ObtenerAsync(id)).ReturnsAsync(usuario);
            mockMapper.Setup(m => m.Map<ObtenerUsuarioDto>(usuario)).Returns(expectedDto);

            var service = new UsuarioService(mockMapper.Object, mockRepository.Object);

            // Act
            var result = await service.ObtenerAsync(id);

            // Assert
            Assert.Equal(expectedDto, result);
            mockRepository.Verify(r => r.ObtenerAsync(id), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ValidCredentials_ReturnsTrue()
        {
            // Arrange
            var email = "test@example.com";
            var clave = "password123";

            var mockRepository = new Mock<IUsuarioRepository>();
            mockRepository.Setup(r => r.LoginAsync(email, clave)).ReturnsAsync(true);

            var mockMapper = new Mock<IMapper>();
            var service = new UsuarioService(mockMapper.Object, mockRepository.Object);

            // Act
            var result = await service.LoginAsync(email, clave);

            // Assert
            Assert.True(result);
            mockRepository.Verify(r => r.LoginAsync(email, clave), Times.Once);
        }
    }
}
