using Features.Clientes;
using Features.Tests._02___Fixtures;
using MediatR;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Features.Tests._06___AutoMock
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteTestsAutoMock
    {
        private readonly ClienteTestsFixture _clienteFixture;
        public ClienteTestsAutoMock(ClienteTestsFixture clienteFixture)
        {
            _clienteFixture = clienteFixture;
        }

        [Fact(DisplayName = "Adicionar Cliente com AutoMock e Sucesso")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteFixture.GerarClienteValido();

            var mocker = new AutoMocker();
            var clienteService = mocker.CreateInstance<ClienteService>();

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            mocker.GetMock<IClienteRepository>().Verify(x => x.Adicionar(cliente), Times.Once);
            //mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }
    }
}
