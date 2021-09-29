using MediatR;
using NerdStore.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.Events
{
    public class PedidoItemAdicionadoEvent : Event
    {

        public Guid ClienteId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public Guid PedidoId { get; private set; }
        public string ProdutoNome { get; set; }
        public decimal ValorUnitario { get; private set; }
        public int Quantidade { get; private set; }
        public PedidoItemAdicionadoEvent(Guid clienteId, Guid produtoId, Guid pedidoId, string produtoNome, decimal valorUnitario, int quantidade)
        {
            ClienteId = clienteId;
            ProdutoId = produtoId;
            PedidoId = pedidoId;
            ProdutoNome = produtoNome;
            ValorUnitario = valorUnitario;
            Quantidade = quantidade;
        }
    }
}
