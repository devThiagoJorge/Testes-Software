using Xunit;

namespace Demo.Tests
{
    public class AssertStringTests
    {
        [Fact]
        public void StringsTools_UnirNomes_RetornarNomeCompleto()
        {
            // Arrange
            var stringTools = new StringsTools();

            // Act
            var nomeCompleto = stringTools.UnirNomes("Thiago", "Jorge");

            // Asserts
            Assert.Equal("Thiago Jorge", nomeCompleto);
        }

        [Fact]
        public void StringsTools_UnirNomes_DeveIgnorarCase()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.UnirNomes("Thiago", "Jorge");

            // Assert
            Assert.Equal("Thiago Jorge", nomeCompleto, true);
        }



        [Fact]
        public void StringsTools_UnirNomes_DeveConterTrecho()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.UnirNomes("Thiago", "Jorge");

            // Assert
            Assert.Contains("orge", nomeCompleto);
        }


        [Fact]
        public void StringsTools_UnirNomes_DeveComecarCom()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.UnirNomes("Thiago", "Jorge");

            // Assert
            Assert.StartsWith("Thi", nomeCompleto);
        }


        [Fact]
        public void StringsTools_UnirNomes_DeveAcabarCom()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.UnirNomes("Thiago", "Jorge");

            // Assert
            Assert.EndsWith("orge", nomeCompleto);
        }


        [Fact]
        public void StringsTools_UnirNomes_ValidarExpressaoRegular()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.UnirNomes("Thiago", "Jorge");

            // Assert
            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", nomeCompleto);
        }

    }
}
