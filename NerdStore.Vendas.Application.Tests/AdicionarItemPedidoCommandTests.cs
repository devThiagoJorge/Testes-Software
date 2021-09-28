using NerdStore.Vendas.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NerdStore.Vendas.Application.Tests
{
    public class AdicionarItemPedidoCommandTests
    {
        [Fact(DisplayName = "Adicionar Item Command Valido")]
        public void AdicionarItemPedidoCommand_CommandEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var command = new AdicionarItemPedidoCommand(Guid.NewGuid(), Guid.NewGuid(), "Produto Teste", 2, 100);
            // Act
            var result = command.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Adicionar Item Command Inválido")]
        public void AdicionarItemPedidoCommand_CommandEstaInvalido_DeveFalharNaValidacao()
        {
            // Arrange
            var command = new AdicionarItemPedidoCommand(Guid.Empty, Guid.Empty, "", 0, 0);

            // Act
            var result = command.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(AdicionarItemPedidoValidation.IdClienteErroMsg, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.IdProdutoErroMsg, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.NomeErroMsg, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.ValorErroMsg, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.QtdMinErroMsg, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }

        [Fact(DisplayName = "Adicionar Item Command Inválido, pois quantidade é maior que o limite")]
        public void AdicionarItemPedidoCommand_CommandEstaInvalido_DeveFalharNaValidacaoPoisQuantidadeEMaior()
        {
            // Arrange
            var command = new AdicionarItemPedidoCommand(Guid.NewGuid(), Guid.NewGuid(), "Produto", 17, 2);

            // Act
            var result = command.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(AdicionarItemPedidoValidation.QtdMaxErroMsg, command.ValidationResult.Errors.Select(x => x.ErrorMessage));

        }
    }
}
