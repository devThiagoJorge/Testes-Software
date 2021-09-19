using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Domain.Models
{
    public class PedidoItem
    {
        public PedidoItem(Guid id, string produtoNome, int quantidade, decimal valorUnitario)
        {
            Id = id;
            ProdutoNome = produtoNome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public Guid Id { get; private set; }
        public string ProdutoNome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
        

        public void ContarQuantidade(int quantidade)
        {
            Quantidade += quantidade;
        }

        public decimal CalcularValor()
        {
            return Quantidade * ValorUnitario;
        }
    }

}
