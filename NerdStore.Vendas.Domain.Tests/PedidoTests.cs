using NerdStore.Core.DomainObjects;
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
    }
}
