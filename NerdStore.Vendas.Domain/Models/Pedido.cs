﻿using NerdStore.Vendas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.Vendas.Domain.Models
{
    public class Pedido
    {
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

        public void AdicionarItem(PedidoItem pedidoItem)
        {
            if(_pedidoItem.Any(x => x.Id == pedidoItem.Id))
            {
                var itemExistente = _pedidoItem.FirstOrDefault(x => x.Id == pedidoItem.Id);

                itemExistente.ContarQuantidade(pedidoItem.Quantidade); // Foi necessário, porq as propriedades são privadas.
                pedidoItem = itemExistente;
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