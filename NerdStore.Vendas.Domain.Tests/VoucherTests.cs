using NerdStore.Vendas.Domain.Enums;
using NerdStore.Vendas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    public class VoucherTests
    {
        [Fact(DisplayName = "Validar voucher do tipo valor e validar se é válido.")]
        public void Voucher_ValidarVoucherTipoValor_DeveEstarValido()
        {
            // Arrange
            var voucher = new Voucher("Promo-15-Reais", 15, null, 10, TipoDescontoVoucher.Valor, DateTime.Now.AddDays(15), true, false);

            // Act
            var result = voucher.ValidarSeAplicavel();

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Validar se o voucher de tipo valor não é válido")]
        public void Voucher_ValidarVoucherTipoValor_DeveSerInvalido()
        {
            // Arrange
            var voucher = new Voucher("", null, null, 0, TipoDescontoVoucher.Valor, DateTime.Now.AddDays(-10), false, true);
            
            // Act
            var result = voucher.ValidarSeAplicavel();

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(6, result.Errors.Count);
            Assert.Contains(VoucherAplicavelValidation.AtivoErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.CodigoErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.DataValidadeErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.QuantidadeErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.UtilizadoErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.ValorDescontoErroMsg, result.Errors.Select(x => x.ErrorMessage));
        }

        [Fact(DisplayName = "Valiar se o voucher percentual de desconto é válido")]
        public void Voucher_ValidarVoucherPercentual_DeveEstarValido()
        {
            // Arrange
            var voucher = new Voucher("Promo-15-Reais", null, 15, 10, TipoDescontoVoucher.Porcentagem, DateTime.Now.AddDays(15), true, false);

            // Act
            var result = voucher.ValidarSeAplicavel();

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Validar se o voucher percentual de desconto não é válido")]
        public void Voucher_ValidarVoucherTipoPercentualDeDesconto_DeveSerInvalido()
        {
            // Arrange
            var voucher = new Voucher("", null, null, 0, TipoDescontoVoucher.Porcentagem, DateTime.Now.AddDays(-10), false, true);

            // Act
            var result = voucher.ValidarSeAplicavel();

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(6, result.Errors.Count);
            Assert.Contains(VoucherAplicavelValidation.AtivoErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.CodigoErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.DataValidadeErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.QuantidadeErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.UtilizadoErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.PercentualDescontoErroMsg, result.Errors.Select(x => x.ErrorMessage));
        }
    }
}
