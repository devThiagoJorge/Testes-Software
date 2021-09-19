using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Domain.Enums
{
    public enum PedidoStatus
    {
        Rascunho = 0,
        Iniciado = 1,
        Pago = 4,
        Entregue = 5,
        Cancelado = 6
    }
}
