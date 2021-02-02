using Newtonsoft.Json;
using SolariPDV.Models.Comercial;
using SolariPDV.Models.Financeiro;
using SolariPDV.Services.Comercial;
using SolariPDV.Services.Financeiro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SolariPDV.ViewModels.PedidoComercial
{

    public class PedidoComercialViewModel : ViewModelBase
    {
        ObservableCollection<ClientesModel> _lstClientes;
        public ObservableCollection<ClientesModel> LstClientes { get { return _lstClientes; } set { SetValue(ref _lstClientes, value); } }

        ClientesModel _clienteSelected;
        public ClientesModel ClienteSelected { get { return _clienteSelected; } set { SetValue(ref _clienteSelected, value); } }

        ObservableCollection<CondicaoRecebimentoModel> _lstCondicao;
        public ObservableCollection<CondicaoRecebimentoModel> LstCondicao { get { return _lstCondicao; } set { SetValue(ref _lstCondicao, value); } }

        CondicaoRecebimentoModel _condicaoSelected;
        public CondicaoRecebimentoModel CondicaoSelected { get { return _condicaoSelected; } set { SetValue(ref _condicaoSelected, value); } }

        public PedidoComercialViewModel()
        {
            GetClientes();
            GetCondicoes();
        }


        private async void GetCondicoes()
        {
            try
            {
                var condL = new CondicaoRecebimentoLogic();
                var jsonCond = await condL.GetCondicoes();
                LstCondicao = JsonConvert.DeserializeObject<ObservableCollection<CondicaoRecebimentoModel>>(jsonCond);
                CondicaoSelected = LstCondicao.Where(c => c.DS_CONDRECEB.ToLower().Contains("vista")).FirstOrDefault();
            }
            catch { }
        }

        private async void GetClientes()
        {
            try
            {
                var cliL = new ClientesLogic();
                var jsonCli = await cliL.GetClientes("");
                LstClientes = JsonConvert.DeserializeObject<ObservableCollection<ClientesModel>>(jsonCli);
                ClienteSelected = LstClientes.Where(c => c.DS_RAZAO.ToLower().Contains("consumidor")).FirstOrDefault();
            }
            catch { }
        }


    }
}
