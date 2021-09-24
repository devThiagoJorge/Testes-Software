using NerdStore.Core.DomainObjects;
using NerdStore.Vendas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.Vendas.Domain.Models
{
    public class Pedido
    {
        public static int MAX_UNIDADES_ITEM => 15;
        public static int MIN_UNIDADES_ITEM => 0;

        protected Pedido()
        {
            _pedidoItem = new List<PedidoItem>();
        }
        public decimal ValorTotal { get; private set; }
        public Guid ClientId { get; private set; }
        public PedidoStatus PedidoStatus { get; private set; }

        private readonly List<PedidoItem> _pedidoItem;
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItem;

        public void CalcularValorPedido()
        {
            ValorTotal = PedidoItems.Sum(x => x.CalcularValor());
        }

        public bool ValidarPedido(PedidoItem pedidoItem)
        {
            if (pedidoItem.Quantidade > 0 && pedidoItem.Quantidade < 15)
            {
                AdicionarItem(pedidoItem);
                return true;
            }

            return false;
        }

        private bool PedidoItemExistente(PedidoItem pedidoItem)
        {
            return _pedidoItem.Any(x => x.Id == pedidoItem.Id); 
        }

        public void AdicionarItem(PedidoItem pedidoItem)
        {
            if (pedidoItem.Quantidade > MAX_UNIDADES_ITEM) throw new DomainException($"Máximo de ${MAX_UNIDADES_ITEM} unidades por produto");

            if(PedidoItemExistente(pedidoItem))
            {
                var itemExistente = _pedidoItem.FirstOrDefault(x => x.Id == pedidoItem.Id);

                itemExistente.ContarQuantidade(pedidoItem.Quantidade); // Foi necessário, porq as propriedades são privadas.
                pedidoItem = itemExistente;

                if(pedidoItem.Quantidade > MAX_UNIDADES_ITEM) throw new DomainException($"Máximo de ${MAX_UNIDADES_ITEM} unidades por produto");

                _pedidoItem.Remove(itemExistente);
            }
           
            _pedidoItem.Add(pedidoItem);

            CalcularValorPedido();
        }

        public void TornarRascunho()
        {
            PedidoStatus = PedidoStatus.Rascunho;
        }

        public static class PedidoFactory
        {
            public static Pedido NovoPedidoRascunho(Guid clienteId)
            {
                var pedido = new Pedido
                {
                    ClientId = clienteId
                };

                pedido.TornarRascunho();

                return pedido;
            }
        }
    }
}
