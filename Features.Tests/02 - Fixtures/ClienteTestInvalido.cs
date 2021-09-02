using Features.Clientes;
using System;
using Xunit;

namespace Features.Tests._02___Fixtures
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteTestInvalido
    {
        private readonly ClienteTestsFixture _clientesTestsFixture;

        public ClienteTestInvalido(ClienteTestsFixture clientesTestsFixture)
        {
            _clientesTestsFixture = clientesTestsFixture;
        }


        [Fact]
        public void Cliente_NovoCliente_DeveSerInvalido()
        {
            // Arrange
            var cliente = _clientesTestsFixture.GerarClienteInvalido();

            // Act
            var result = cliente.EhValido();

            // Asserts
            Assert.False(result);
            Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);
        }
    }
}
