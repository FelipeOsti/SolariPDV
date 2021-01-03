using SolariPDV.Models;
using SolariPDV.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace SolariPDV.ViewModels
{
    public class VerMesasViewModel : ViewModelBase
    {
        ObservableCollection<MesasModel> lstMesas;
        public ObservableCollection<MesasModel> LstMesas { get { return lstMesas; } set { SetValue(ref lstMesas, value); } }


        public VerMesasViewModel()
        {
            GetMesas();
        }

        private async void GetMesas()
        {
            var mesaLogic = new MesaLogic();
            LstMesas = await mesaLogic.getMesas();
        }

        internal async Task<PedidoSistemaModel> GetPedido(MesasModel mesa)
        {
            var mesaLogic = new MesaLogic();
            var lstPedidos = await mesaLogic.GetPedido(mesa.ID_MESA);
            if(lstPedidos != null) return lstPedidos[0];
            return new PedidoSistemaModel() { ID_MESA = mesa.ID_MESA, DS_MESA = mesa.DS_MESA };
        }
    }
}
