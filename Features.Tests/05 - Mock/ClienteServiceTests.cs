using Features.Clientes;
using Features.Tests._02___Fixtures;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Features.Tests._05___Mock
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteServiceTests
    {
        private readonly ClienteTestsFixture _clienteTestBogus; 
        public ClienteServiceTests(ClienteTestsFixture clienteTestBogus )
        {
            _clienteTestBogus = clienteTestBogus;
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestBogus.GerarClienteValido();
            var clienteRepo = new Mock<IClienteRepository>();
            var mediator = new Mock<IMediator>();

            var clienteService = new ClienteService(clienteRepo.Object, mediator.Object);

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            clienteRepo.Verify(x => x.Adicionar(cliente), Times.Once);
            //mediator.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Falha ao adicionar cliente")]
        public void ClienteService_Adicionar_DeveFalharPoisClienteInvalido()
        {
            // Arrange
            var cliente = _clienteTestBogus.GerarClienteInvalido();
            var clienteRepo = new Mock<IClienteRepository>();
            var mediator = new Mock<IMediator>();

            var clienteService = new ClienteService(clienteRepo.Object, mediator.Object);

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            clienteRepo.Verify(x => x.Adicionar(cliente), Times.Never);
            mediator.Verify(p => p.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Deve retornar os clientes ativos")]
        public void ClienteService_ObterAtivos_DeveObterOsClientesAtivos()
        {
            // Arrange
            var clienteRepo = new Mock<IClienteRepository>();
            var mediatr = new Mock<IMediator>();

            clienteRepo.Setup(c => c.ObterTodos())
                .Returns(_clienteTestBogus.GerarVariosClientes());

            var clienteService = new ClienteService(clienteRepo.Object, mediatr.Object);

            // Act
            var result = clienteService.ObterTodosAtivos();

            // Assert
            clienteRepo.Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(result.Any());
            Assert.False(result.Count(c => !c.Ativo) > 0);

        }

    }
}
