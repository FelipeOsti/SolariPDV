using SolariPDV.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolariPDV.ViewModels
{
    public class PedidoInicioPageViewModel : ViewModelBase
    {
        PedidoModel pedido;
        public PedidoModel Pedido { get { return pedido; } set { SetValue(ref pedido, value); } }

        PedidoSistemaModel pedidoSistema;
        public PedidoSistemaModel PedidoSistema{ get { return pedidoSistema; } set { SetValue(ref pedidoSistema, value); } }

        public void SetPedidoSistema(PedidoSistemaModel _ped)
        {
            PedidoSistema = _ped;
        }
    }
}
