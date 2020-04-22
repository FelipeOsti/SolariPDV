using SolariPDV.Models;
using SolariPDV.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SolariPDV.ViewModels
{
    public class VerPedidosViewModel : ViewModelBase
    {
        ObservableCollection<PedidoSistemaModel> lstPedidos;
        public ObservableCollection<PedidoSistemaModel> LstPedidos { get { return lstPedidos; } set { SetValue(ref lstPedidos, value); } }

        bool _bboMesas;
        public bool bboMesas { get { return _bboMesas; } set { SetValue(ref _bboMesas, value); } }
        
        bool _bboSearch;
        public bool bboSearch { get { return _bboSearch; } set { SetValue(ref _bboSearch, value); } }

        DateTime _ddtFinal;
        public DateTime ddtFinal { get { return _ddtFinal; } set { SetValue(ref _ddtFinal, value); } }

        bool _bboFinalizados;
        public bool bboFinalizados { get { return _bboFinalizados; } set { SetValue(ref _bboFinalizados, value); } }

        DateTime _ddtInicial;
        public DateTime ddtInicial { get { return _ddtInicial; } set { SetValue(ref _ddtInicial, value); } }

        string _sdsFiltro;
        public string sdsFiltro { get { return _sdsFiltro; } set { SetValue(ref _sdsFiltro, value); } }

        public string sVL_TOTAL
        { 
            get
            {
                if (lstPedidos == null) return "R$ 0,00";
                double total = 0;
                foreach (var ped in lstPedidos) total += ped.VL_TOTAL;
                return total.ToString("C");
            }
        }



        public VerPedidosViewModel()
        {
            bboFinalizados = false;
            bboSearch = false;
            ddtInicial = DateTime.Now.Date.AddMonths(-1);
            ddtFinal = DateTime.Now.Date;
            sdsFiltro = "";

            IdentificarMesas();            
            GetPedidosCommand = new Command(BuscarInformacoesIniciais);
        }

        ICommand _GetPedidosCommand;
        public ICommand GetPedidosCommand { get { return _GetPedidosCommand; } set { SetValue(ref _GetPedidosCommand, value); } }

        public async void BuscarInformacoesIniciais()
        {
            try
            {
                IsBusy = true;
                try
                {                                        
                    var pedLogic = new PedidoLogic();
                    LstPedidos = await pedLogic.GetPedidos(sdsFiltro,ddtInicial,ddtFinal,bboFinalizados, bboSearch);
                }
                catch
                {
                }
            }           
            finally
            {
                IsBusy = false;
            }
        }

        private async void IdentificarMesas()
        {
            try
            {
                MesaLogic mL = new MesaLogic();
                var mesas = await mL.getMesas();
                bboMesas = false;
                if (mesas?.Count > 0) bboMesas = true;
            }
            catch
            {

            }
        }
    }
}
