using Xunit;

namespace Demo.Tests
{
    public class AssertingCollectionsTests
    {
        [Fact]
        public void Funcionario_Habilidades_NaoDevePossuirHabilidadesVazias()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Thiago", 10000);

            // Assert
            Assert.All(funcionario.Habilidades, 
                habilidade => Assert.False(string.IsNullOrEmpty(habilidade)));

        }

        [Fact]
        public void Funcionario_Habilidades_JuniorDevePossuirHabilidadesBasicas()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Thiago", 1000);

            // Assert 
            Assert.Contains("OOP", funcionario.Habilidades);
        }

        [Fact]
        public void Funcionario_Habilidades_JuniorNaoDevePossuirHabilidadesAvancadas()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Thiago", 1000);

            // Assert 
            Assert.DoesNotContain("Microservices", funcionario.Habilidades);
        }

        [Fact]
        public void Funcionario_Habilidades_PlenoDevePossuirTodasMenosMicrosservices()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Thiago", 7900);
            var habilidadesPleno = new[]
            {
                "Lógica de Programação",
                "OOP",
                "Testes",
            };

            // Assert
            Assert.Equal(habilidadesPleno, funcionario.Habilidades);
        }

        [Fact]
        public void Funcionario_Habilidades_SeniorDevePossuirTodasHabilidades()
        {
            // Arrange & Act
            var funcionario = FuncionarioFactory.Criar("Thiago", 15000);

            var habilidadesBasicas = new[]
            {
                "Lógica de Programação",
                "OOP",
                "Testes",
                "Microservices"
            };

            // Assert
            Assert.Equal(habilidadesBasicas, funcionario.Habilidades);
        }
    }
}
