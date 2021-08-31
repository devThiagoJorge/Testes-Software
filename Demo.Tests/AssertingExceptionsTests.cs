using System;
using Xunit;

namespace Demo.Tests
{
    public class AssertingExceptionsTests
    {

        [Fact]
        public void Funcionario_Salario_DeveRetornarErroSalarioInferiorPermitido()
        {
            // Arrange & Act & Assert
            var exception =
                Assert.Throws<Exception>(() => FuncionarioFactory.Criar("Eduardo", 250));

            Assert.Equal("Salário inferior ao permitido", exception.Message);
        }
    }

}
