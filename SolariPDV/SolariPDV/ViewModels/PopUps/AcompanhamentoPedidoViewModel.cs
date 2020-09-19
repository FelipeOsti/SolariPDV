using SolariPDV.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SolariPDV.ViewModels.PopUps
{
    public class AcompanhamentoPedidoViewModel : ViewModelBase
    {
        PedidoSistemaModel pedido;
        public PedidoSistemaModel Pedido{ get { return pedido; } set { pedido = value; OnPropertyChanged(); } }

        ObservableCollection<ItemPedidoModel> lstItens;
        public ObservableCollection<ItemPedidoModel> LstItens { get { return lstItens; } set { lstItens = value; OnPropertyChanged(); } }

        public AcompanhamentoPedidoViewModel(PedidoSistemaModel _pedido)
        {
            Pedido = _pedido;
            LstItens = Pedido.itens;

            foreach (var it in LstItens)
            {
                it.GetFichaMaterial();
            }
        }
    }
}
