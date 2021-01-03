using SolariPDV.Models;
using SolariPDV.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SolariPDV.ViewModels
{
    public class AcompanharPedidoViewModel : ViewModelBase
    {
        ObservableCollection<PedidoSistemaModel> _lstPedidos;

        public ObservableCollection<PedidoSistemaModel> LstPedidos { get { return _lstPedidos; } set { SetValue(ref _lstPedidos, value); } }
        public System.Timers.Timer timer;

        bool _nenhumPedido;
        public bool nenhumPedido { get { return _nenhumPedido; } set { SetValue(ref _nenhumPedido, value); } }

        public AcompanharPedidoViewModel()
        {
            GetPedidosCommand = new Command(BuscarInformacoesIniciais);
            timer = new System.Timers.Timer();
            timer.Interval = 30000;
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            GetPedidosCommand.Execute(null);
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
                    var ddtInicial = DateTime.Now.Date;
                    var ddtFinal = DateTime.Now.Date;

                    var pedLogic = new PedidoLogic();
                    LstPedidos = await pedLogic.GetPedidos("", ddtInicial, ddtFinal, false, true);

                    LstPedidos = new ObservableCollection<PedidoSistemaModel>(LstPedidos.Where(ped => string.IsNullOrEmpty(ped.DT_FIMPRODUCAO) &&
                                                                                                      Convert.ToDateTime(ped.DT_PREVENTREGA) < DateTime.Now.AddMinutes(75))
                                                                                              .ToList().OrderBy(ped => ped.ID_PEDIDO));

                    var i = 1;
                    foreach (var ped in LstPedidos)
                    {
                        ped.sdsOrdem = i + "º";
                        MontarItems(ped, i);
                        i++;
                    }
                }
                catch
                {
                }
            }
            finally
            {
                IsBusy = false;

                if (LstPedidos.Count > 0)
                    nenhumPedido = false;
                else
                    nenhumPedido = true;
            }
        }

        private async void MontarItems(PedidoSistemaModel ped, int index)
        {
            try
            {
                var pedLogic = new PedidoLogic();
                ped.itens = await pedLogic.GetItemPedido(ped.ID_PEDIDO);
                foreach (var item in ped.itens)
                {
                    var nqtQtde = item.DS_MATERIAL.Substring(0, 5).Trim() == "" ? " " : item.QT_PEDIDO.ToString();
                    ped.DS_ITENS += nqtQtde + " " + item.DS_MATERIAL + "\n";
                }
                LstPedidos[index - 1] = ped;
            }
            catch { }
        }

        internal async void IniciarFinalizarPedido(long nidPedido, string sdsAcao)
        {
            try
            {
                var retorno = "";
                var pedLogic = new PedidoLogic();
                if (sdsAcao == "I")
                    retorno = await pedLogic.IniciarCozinha(nidPedido);
                else
                    retorno = await pedLogic.FinalizarCozinha(nidPedido);
                retorno = retorno.Replace("\"", "");
                if (sdsAcao == "F" && retorno == "OK")
                {
                    LstPedidos.Remove(LstPedidos.FirstOrDefault(p => p.ID_PEDIDO == nidPedido));
                }
                else if (sdsAcao == "I" && retorno == "OK")
                {
                    LstPedidos.FirstOrDefault(p => p.ID_PEDIDO == nidPedido).DT_INICIOPRODUCAO = DateTime.Now.ToString();
                }

                if (retorno != "OK") new Exception(retorno);
            }
            catch { throw; }
        }
    }
}
