using NerdStore.Vendas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Domain.Repositories
{
    public interface IPedidoRepository
    {
        void Adicionar(Pedido pedido);
    }
}
