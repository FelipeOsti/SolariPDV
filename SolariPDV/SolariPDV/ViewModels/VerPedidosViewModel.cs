using SolariPDV.Models;
using SolariPDV.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        
        bool _nenhumPedido;
        public bool nenhumPedido { get { return _nenhumPedido; } set { SetValue(ref _nenhumPedido, value); } }

        DateTime _ddtFinal;
        public DateTime ddtFinal { get { return _ddtFinal; } set { SetValue(ref _ddtFinal, value); } }

        bool _bboFinalizados;
        public bool bboFinalizados { get { return _bboFinalizados; } set { SetValue(ref _bboFinalizados, value); } }

        DateTime _ddtInicial;
        public DateTime ddtInicial { get { return _ddtInicial; } set { SetValue(ref _ddtInicial, value); } }

        string _sdsFiltro;
        public string sdsFiltro { get { return _sdsFiltro; } set { SetValue(ref _sdsFiltro, value); } }

        double _nvlTotal;
        public double VL_TOTAL {
            get
            {
                return _nvlTotal;                
            }
            set
            {
                SetValue(ref _nvlTotal, value);
            }
        }

        public VerPedidosViewModel()
        {
            bboFinalizados = false;
            bboSearch = false;
            ddtInicial = DateTime.Now.Date;
            ddtFinal = DateTime.Now.Date;
            sdsFiltro = "";
            nenhumPedido = false;

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
                    LstPedidos = await pedLogic.GetPedidos(sdsFiltro, ddtInicial, ddtFinal, bboFinalizados, true);
                    VL_TOTAL = 0;
                    if (LstPedidos != null)
                    {
                        nenhumPedido = false;
                        LstPedidos = new ObservableCollection<PedidoSistemaModel>(LstPedidos.OrderBy(ped => Convert.ToDateTime(string.IsNullOrEmpty(ped.DT_PREVENTREGA) ? DateTime.Now.ToString() : ped.DT_PREVENTREGA)));                        
                        foreach (var ped in LstPedidos) VL_TOTAL += ped.VL_TOTAL;
                    }
                    else
                        nenhumPedido = true;                    
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
