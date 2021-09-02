using Features.Clientes;
using System;
using Xunit;

namespace Features.Tests._01___Traits
{
    public class ClienteTraitsTests
    {
        [Fact(DisplayName = "Traits cliente deve ser válido")]
        [Trait("Categoria", "Clientes válido")]
        public void Cliente_NovoCliente_DeveSerValido()
        {
            // Arrange
            var cliente = new Cliente(
                Guid.NewGuid(),
                "Thiago",
                "Jorge",
                DateTime.Now.AddYears(-21),
                "thiagojorge.fatec@gmail.com",
                true,
                DateTime.Now);

            // Act
            var result = cliente.EhValido();

            // Asserts
            Assert.True(result);
            Assert.Equal(0, cliente.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "Cliente deve ser inválido")]
        [Trait("Categoria", "Clientes inválido")]
        public void Cliente_NovoCliente_DeveSerInvalido()
        {
            // Arrange
            var cliente = new Cliente(
                Guid.NewGuid(),
                "",
                "",
                DateTime.Now.AddYears(-21),
                "",
                true,
                DateTime.Now);

            // Act
            var result = cliente.EhValido();

            // Asserts
            Assert.False(result);
            Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);
        }
    }
}
