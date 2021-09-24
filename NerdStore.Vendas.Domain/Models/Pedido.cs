﻿using NerdStore.Core.DomainObjects;
using NerdStore.Vendas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.Vendas.Domain.Models
{
    public class Pedido
    {
        public static int MAX_UNIDADES_ITEM => 15;
        public static int MIN_UNIDADES_ITEM => 1;

        protected Pedido()
        {
            _pedidoItem = new List<PedidoItem>();
        }
        public decimal ValorTotal { get; private set; }
        public Guid ClientId { get; private set; }
        public PedidoStatus PedidoStatus { get; private set; }

        private readonly List<PedidoItem> _pedidoItem;
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItem;

        private void CalcularValorPedido()
        {
            ValorTotal = PedidoItems.Sum(x => x.CalcularValor());
        }

        private bool ValidarPedido(PedidoItem pedidoItem)
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

        private void ValidarQuantidadeMaxima(PedidoItem pedidoItem)
        {
            if (pedidoItem.Quantidade > MAX_UNIDADES_ITEM) throw new DomainException($"Máximo de ${MAX_UNIDADES_ITEM} unidades por produto");
        }
        private PedidoItem RecuperarItem(PedidoItem pedidoItem)
        {
            return _pedidoItem.FirstOrDefault(x => x.Id == pedidoItem.Id);
        }

        public void AdicionarItem(PedidoItem pedidoItem)
        {
            ValidarQuantidadeMaxima(pedidoItem);

            if (PedidoItemExistente(pedidoItem))
            {
                var itemExistente = RecuperarItem(pedidoItem);
                itemExistente.ContarQuantidade(pedidoItem.Quantidade); // Foi necessário, porq as propriedades são privadas.
                pedidoItem = itemExistente;

                ValidarQuantidadeMaxima(pedidoItem);

                _pedidoItem.Remove(itemExistente);
            }
          
            _pedidoItem.Add(pedidoItem);

            CalcularValorPedido();
        }

        public void AtualizarPedido(PedidoItem pedidoItem)
        {
            if (!PedidoItemExistente(pedidoItem)) 
                throw new DomainException("O item não está na lista");

            ValidarQuantidadeMaxima(pedidoItem);

            var pedidoExistente = RecuperarItem(pedidoItem);
            _pedidoItem.Remove(pedidoExistente);
            _pedidoItem.Add(pedidoItem);

            CalcularValorPedido();
        }

        public void RemoverItem(PedidoItem pedidoItem)
        {
            if (!PedidoItemExistente(pedidoItem))
                throw new DomainException("O item não está na lista");

            _pedidoItem.Remove(pedidoItem);

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
