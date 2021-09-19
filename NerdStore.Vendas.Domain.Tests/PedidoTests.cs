using NerdStore.Vendas.Domain.Models;
using System;
using System.Linq;
using Xunit;
using static NerdStore.Vendas.Domain.Models.Pedido;

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

        [Fact(DisplayName = "Mudar")]
        [Trait("Categoria", "Mudar")]
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
    }
}
