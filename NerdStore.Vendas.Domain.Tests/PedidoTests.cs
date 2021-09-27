using NerdStore.Core.DomainObjects;
using NerdStore.Vendas.Domain.Enums;
using NerdStore.Vendas.Domain.Models;
using System;
using System.Linq;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{

    public class PedidoTests
    {
        [Fact(DisplayName = "Adicionar Item: Novo Pedido")]
        public void AdicionarItemPedido_NovoPedido_DeveAtualizarValor()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var itemPedido = new PedidoItem(Guid.NewGuid(), "Nescau", 2, 10);

            // Act
            pedido.AdicionarItem(itemPedido);

            // Assert
            Assert.Equal(20, pedido.ValorTotal);
                
        }

        [Fact(DisplayName = "Adicionar item ao carrinho (mesmo produto)")]
        public void AdicionarItemPedido_ItemExistente_DeveIncrementarUnidadeSomandoOsValores()
        {
            // Arrange
            var produtoId = new Guid();
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var itemPedido = new PedidoItem(produtoId, "Nescau", 2, 10);
            var itemPedido2 = new PedidoItem(produtoId, "Nescau", 3, 10);

            // Act
            pedido.AdicionarItem(itemPedido);
            pedido.AdicionarItem(itemPedido2);

            // Assert
            Assert.Equal(50, pedido.ValorTotal);
            Assert.Equal(1, pedido.PedidoItems.Count);
            Assert.Equal(5, pedido.PedidoItems.FirstOrDefault(p => p.Id == produtoId).Quantidade);
        }
        
        [Fact(DisplayName = "Não pode adicionar, pois está acima do permitido")]
        public void AdicionarItemPedido_NovoPedido_NaoDeveAdicionarPoisEstaAcimaDoLimite()
        {
            // ARRANGE 
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Nescau", Pedido.MAX_UNIDADES_ITEM + 1, 4);

            // ACT && ASSERT
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem));
            
        }

        [Fact(DisplayName = "Não deve adicionar pois está abaixo do permitido")]
        public void AdicionarItemPedido_NovoPedido_DeveRetornarExceptionPoisEstaAbaixoDoNumeroDeUnidadesPermitidos()
        {
            // ARRANGE & ACT & ASSERT
            Assert.Throws<DomainException>(() => new PedidoItem(Guid.NewGuid(), "Nescau", Pedido.MIN_UNIDADES_ITEM - 1, 1));
            
        }

        [Fact(DisplayName = "Não deve adicionar pois ultrapassa o número de unidades.")]
        public void AdicionarItemPedido_NovoPedido_DeveRetornarExceptionQuandoAdicionoOMesmoItemNovamenteSendoQueEstaAcimaDoLimitePermitido()
        {
            // Arrange
            var idPedido = Guid.NewGuid();
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem = new PedidoItem(idPedido, "Nescau", 1, 2);
            var pedidoItem2 = new PedidoItem(idPedido, "Nescau", Pedido.MAX_UNIDADES_ITEM, 2);
            // Act
            pedido.AdicionarItem(pedidoItem);
            
            // Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem2));
        }

        [Fact(DisplayName = "Não deve atualizar o item, pois item não existe")]
        public void AtualizarItemPedido_AtualizarPedido_NaoDeveAtualizarPedidoPoisItemNaoExiste()
        {
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItemAtualizado = new PedidoItem(Guid.NewGuid(), "Nescau", 1, 10);

            //Arrange & Act & Assert
            Assert.Throws<DomainException>(() => pedido.AtualizarPedido(pedidoItemAtualizado));
        }

        [Fact(DisplayName = "Deve atualizar a quantidade de itens")]
        public void AtualizarItemPedido_ItemValido_DeveAtualizarQuantidade()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var idPedido = Guid.NewGuid();
            var itemPedido = new PedidoItem(idPedido, "Nescau", 1, 10);
            var pedidoItemAtualizado = new PedidoItem(idPedido, "Nescau", 4, 10);
            var quantidadeAtualizado = pedidoItemAtualizado.Quantidade;

            // Act
            pedido.AdicionarItem(itemPedido);
            pedido.AtualizarPedido(pedidoItemAtualizado);

            // Assert
            Assert.Equal(quantidadeAtualizado, pedido.PedidoItems.FirstOrDefault(x => x.Id == idPedido).Quantidade);
        }

        [Fact(DisplayName = "Deve calcular o total do pedido")]
        public void AtualizarItemPedido_ItemValido_DeveCalcularOTotalDoPedido()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var idPedido = Guid.NewGuid();
            var itemPedido = new PedidoItem(Guid.NewGuid(), "Nescau", 1, 10);
            var itemPedido2 = new PedidoItem(idPedido, "Nescau", 1, 10);
            pedido.AdicionarItem(itemPedido);
            pedido.AdicionarItem(itemPedido2);

            var pedidoItemAtualizado = new PedidoItem(idPedido, "Nescau", 4, 10);

            var valorTotalDoPedidoAtualizado = itemPedido.Quantidade * itemPedido.ValorUnitario + pedidoItemAtualizado.ValorUnitario * pedidoItemAtualizado.Quantidade;

            // Act
            
            pedido.AtualizarPedido(pedidoItemAtualizado);

            // Assert
            Assert.Equal(valorTotalDoPedidoAtualizado, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Não deve atualizar pois não está dentro do limite máximo de unidades")]
        public void AtualizarItemPedido_ItemInvalido_NaoDeveAtualizarPoisNaoEstaDentroDoLimiteMaximoPermitido()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var idPedido = Guid.NewGuid();
            var itemPedido = new PedidoItem(idPedido, "Nescau", 1, 10);
            var pedidoAtualizado = new PedidoItem(idPedido, "Nescau", Pedido.MAX_UNIDADES_ITEM + 1, 3);

            // Act
            pedido.AdicionarItem(itemPedido);
            

            // Assert
            Assert.Throws<DomainException>(() => pedido.AtualizarPedido(pedidoAtualizado));
        }

        [Fact(DisplayName = "Não deve atualizar pois não está dentro do limite mínimo de unidades")]
        public void AtualizarItemPedido_ItemInvalido_NaoDeveAtualizarPoisNaoEstaDentroDoLimiteMinimoPermitido()
        {
            // Arrange & Act & Assert
            Assert.Throws<DomainException>(() => new PedidoItem(Guid.NewGuid(), "Nescau", Pedido.MIN_UNIDADES_ITEM - 1, 3));
        }

        [Fact(DisplayName = "Não deve remover, pois o item não existe na lista")]
        public void Pedido_RemoverItem_DeveRetornarExceptionPoisItemNaoFoiExiste()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var itemPedido = new PedidoItem(Guid.NewGuid(), "Nescau", 2, 10);

            // Act & Assert
            Assert.Throws<DomainException>(() => pedido.RemoverItem(itemPedido));
        }

        [Fact(DisplayName = "Deve remover o pedido, pois ele está na lista")]
        public void Pedido_RemoverItem_DeveRemoverPoisEstaNaLista()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var idPedido = Guid.NewGuid();
            var itemPedido = new PedidoItem(idPedido, "Nescau", 2, 10);

            // Act
            pedido.AdicionarItem(itemPedido);
            pedido.RemoverItem(itemPedido);

            // Assert
            Assert.Equal(0, pedido.PedidoItems.Count);
        }

        [Fact(DisplayName = "Deve calcular valor total após remover o pedido")]
        public void Pedido_RemoverItem_DeveCalcularOValorTotalAposRemoverOItem()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var itemPedido = new PedidoItem(Guid.NewGuid(), "Nescau", 2, 10);
            var itemPedido2 = new PedidoItem(Guid.NewGuid(), "Farinha", 4, 5);
            pedido.AdicionarItem(itemPedido);
            pedido.AdicionarItem(itemPedido2);

            var valorTotal = itemPedido2.Quantidade * itemPedido2.ValorUnitario;
            // Act
            pedido.RemoverItem(itemPedido);

            // Assert
            Assert.Equal(valorTotal, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplicar voucher válido")]
        public void Pedido_AplicarVoucher_VoucherEValido()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var voucher = new Voucher("Promo-15-Reais", 15, null, 10, TipoDescontoVoucher.Valor, DateTime.Now.AddDays(15), true, false);

            // Act
            var result = pedido.AplicarVoucher(voucher);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Aplicar voucher inválido")]
        public void Pedido_AplicarVoucher_VoucherInvalidoDeveRetornarFalso()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var voucher = new Voucher("", 15, null, 10, TipoDescontoVoucher.Valor, DateTime.Now.AddDays(15), true, false);

            // Act
            var result = pedido.AplicarVoucher(voucher);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "Calcular total com voucher aplicado (Voucher de valor)")]
        public void Pedido_CalcularTotalAplicandoVoucher_VoucherDeValorValidoDeveAplicarDesconto()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var itemPedido = new PedidoItem(Guid.NewGuid(), "Nescau", 2, 10);
            var voucher = new Voucher("PROMO-15-REAIS", 15, null, 1, TipoDescontoVoucher.Valor, DateTime.Now.AddDays(15), true, false);
            pedido.AdicionarItem(itemPedido);
            // Act
            pedido.AplicarVoucher(voucher);
  
            // Assert
            Assert.Equal(5, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Calcular total com voucher de desconto aplicado (Voucher Percentual de desconto)")]
        public void Pedido_CalcularTotalAplicandoVoucher_VoucherDePercentualValidoDeveAplicarDesconto()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var itemPedido = new PedidoItem(Guid.NewGuid(), "Nescau", 1, 100);
            var itemPedido2 = new PedidoItem(Guid.NewGuid(), "Toddy", 1, 100);
            var voucher = new Voucher("PROMO-20-PORCENTO", null, 20, 1, TipoDescontoVoucher.Porcentagem, DateTime.Now.AddDays(15), true, false);
            pedido.AdicionarItem(itemPedido);
            pedido.AdicionarItem(itemPedido2);
            // Act
            pedido.AplicarVoucher(voucher);

            // Assert
            Assert.Equal(160, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Valor total do pedido deve ser 0, pois desconto do valor é maior que o valor total do pedido")]
        public void Pedido_CalcularTotalAplicandoVoucher_ValorDeDescontoDoVoucherDeValorEMaiorQueOTotalDoPedido()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var itemPedido = new PedidoItem(Guid.NewGuid(), "Nescau", 1, 50);
            var itemPedido2 = new PedidoItem(Guid.NewGuid(), "Toddy", 1, 40);
            var voucher = new Voucher("PROMO-100-REAIS", 100, null, 1, TipoDescontoVoucher.Valor, DateTime.Now.AddDays(15), true, false);
            pedido.AdicionarItem(itemPedido);
            pedido.AdicionarItem(itemPedido2);

            // Act
            pedido.AplicarVoucher(voucher);

            // Assert
            Assert.Equal(0, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Valor total do pedido deve ser 0, pois o percentual de desconto é maior que o valor total.")]
        public void Pedido_CalcularTotalAplicandoVoucher_ValorDeDescontoDoVoucherDeValorEMaiorQueOTotalDoPedidoPoisPercentualCobreValor()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var itemPedido = new PedidoItem(Guid.NewGuid(), "Nescau", 1, 50);
            var itemPedido2 = new PedidoItem(Guid.NewGuid(), "Toddy", 1, 50);
            var voucher = new Voucher("PROMO-100-REAIS", null, 100, 1, TipoDescontoVoucher.Porcentagem, DateTime.Now.AddDays(15), true, false);
            pedido.AdicionarItem(itemPedido);
            pedido.AdicionarItem(itemPedido2);

            // Act
            pedido.AplicarVoucher(voucher);

            // Assert
            Assert.Equal(0, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Deve calcular o total com desconto se o pedido for atualizado")]
        public void Pedido_CalcularTotalAplicandoVoucher_DeveCalcularValorTotalComDescontosAposAtualizarLista()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var idPedido = Guid.NewGuid();
            var itemPedido = new PedidoItem(idPedido, "Nescau", 3, 10);
            pedido.AdicionarItem(itemPedido);
            var itemPedido2 = new PedidoItem(Guid.NewGuid(), "Toddy", 2, 6);

            var voucher = new Voucher("PROMO-10-REAIS", 10, null, 1, TipoDescontoVoucher.Valor, DateTime.Now.AddDays(14), true, false);

            // Act
            pedido.AplicarVoucher(voucher);
            pedido.AdicionarItem(itemPedido2);

            // Assert
            var totalEsperado = pedido.PedidoItems.Sum(x => x.ValorUnitario * x.Quantidade) - voucher.ValorDesconto;
            Assert.Equal(totalEsperado, pedido.ValorTotal);
        }
    }
}
