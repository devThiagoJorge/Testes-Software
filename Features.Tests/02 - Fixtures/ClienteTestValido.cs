using Features.Clientes;
using System;
using Xunit;

namespace Features.Tests._02___Fixtures
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteTestValido
    {
        private readonly ClienteTestsFixture _clienteTestsFixture;

        public ClienteTestValido(ClienteTestsFixture clienteTestsFixture)
        {
            _clienteTestsFixture = clienteTestsFixture;
        }

        [Fact]
        public void Cliente_NovoCliente_DeveSerValido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteValido();

            // Act
            var result = cliente.EhValido();

            // Asserts
            Assert.True(result);
            Assert.Equal(0, cliente.ValidationResult.Errors.Count);
        }

        
    }
}
