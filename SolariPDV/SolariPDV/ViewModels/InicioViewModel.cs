using Newtonsoft.Json;
using SolariPDV.Models;
using SolariPDV.Models.Comercial;
using SolariPDV.Services;
using SolariPDV.Services.Comercial;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SolariPDV.ViewModels
{
    public class InicioViewModel : ViewModelBase
    {
        double nvlVendido;
        public double VL_VENDIDO { get { return nvlVendido; } set { SetValue(ref nvlVendido, value); } }

        double nqtItens;
        public double QT_ITENS { get { return nqtItens; } set { SetValue(ref nqtItens, value); } }

        public ICommand GetDashBoardCommand;

        public InicioViewModel()
        {
            GetDashBoardCommand = new Command(BuscarDados);
        }

        private async void BuscarDados()
        {
            try
            {
                DashboardLogic dL = new DashboardLogic();
                var json = await dL.GetPedidosMes();

                var pedMes = JsonConvert.DeserializeObject<List<DashboardPedidosMesModel>>(json);
                if (pedMes?.Count > 0 && App.current.bboDashboard)
                {
                    VL_VENDIDO = pedMes[0].VL_TOTAL;
                    QT_ITENS = pedMes[0].QT_TOTITEM;
                }
                else
                {                    
                    VL_VENDIDO = 0;
                    QT_ITENS = 0;
                }
            }
            catch { }
        }
    }
}
