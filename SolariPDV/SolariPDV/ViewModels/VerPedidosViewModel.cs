using SolariPDV.Models;
using SolariPDV.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SolariPDV.ViewModels
{
    public class VerPedidosViewModel : ViewModelBase
    {
        ObservableCollection<PedidoSistemaModel> lstPedidos;
        public ObservableCollection<PedidoSistemaModel> LstPedidos { get { return lstPedidos; } set { SetValue(ref lstPedidos, value); } }

        public VerPedidosViewModel()
        {
            BuscarInformacoesIniciais();
        }

        public async void BuscarInformacoesIniciais()
        {
            try
            {
                IsBusy = true;
                var pedLogic = new PedidoLogic();
                LstPedidos = await pedLogic.GetPedidosPendentes();
            }
            catch
            {

            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
