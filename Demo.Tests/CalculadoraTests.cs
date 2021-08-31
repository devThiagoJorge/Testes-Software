using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo.Tests
{
    public class CalculadoraTests
    {
        [Fact]
        public void Calculadora_Somar_RetornarValorDaSoma()
        { 
            //Arrange
            var calculadora = new Calculadora();

            //Act
            var resultado = calculadora.Somar(3, 3);

            //Assert
            Assert.Equal(6, resultado);
        }

        [Fact]
        public void Calculadora_Dividir_RetornarValorDaDivisao()
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            var resultado = calculadora.Dividir(4, 2);

            //Assert
            Assert.Equal(2, resultado);
        }

        [Theory]
        [InlineData(1,1,2)]
        [InlineData(3,3,6)]
        [InlineData(10,10,20)]
        public void Calculadora_Somar_RetornarValoresSomaCorreta(double v1, double v2, double total)
        {
            //Arrange
            var calculadora = new Calculadora();

            //Act
            var resultado = calculadora.Somar(v1, v2);

            // Assert
            Assert.Equal(total, resultado);
        }
    }
}
