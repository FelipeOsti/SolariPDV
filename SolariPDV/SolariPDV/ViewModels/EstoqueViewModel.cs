using Newtonsoft.Json;
using SolariPDV.Models;
using SolariPDV.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SolariPDV.ViewModels
{
    public class EstoqueViewModel : ViewModelBase
    {

        ObservableCollection<EstoqueModel> _lstEstoque;
        public ObservableCollection<EstoqueModel> lstEstoque { get { return _lstEstoque; } set { SetValue(ref _lstEstoque, value); } }

        ICommand _GetEstoqueCommand;
        public ICommand GetEstoqueCommand { get { return _GetEstoqueCommand; } set { SetValue(ref _GetEstoqueCommand, value); } }


        public EstoqueViewModel()
        {
            EscanearCodigoBarrasCommand = new Command(EscanearCodigoBarras);
            AdicionarFamiliaCommand = new Command(AdicionarFamilia);
            GetEstoqueCommand = new Command(RecuperarEstoque);
        }

        private async void RecuperarEstoque(object obj)
        {
            await BuscarProduto(null);
        }

        private async void AdicionarFamilia(object obj)
        {
            EstoqueLogic el = new EstoqueLogic();
            var familia = await Application.Current.MainPage.DisplayPromptAsync("Nova Familia", "Qual o nome da nova Familia?");
            if (!String.IsNullOrEmpty(familia))
            await el.SalvarFamilia(0, familia);
        }

        ICommand _addFamiliaCommand;
        public ICommand AdicionarFamiliaCommand {get { return _addFamiliaCommand; } set { SetValue(ref _addFamiliaCommand, value); } }

        public ICommand EscanearCodigoBarrasCommand;

        public async void EscanearCodigoBarras()
        {
            var scanner = DependencyService.Get<IQrCodeScanningService>();
            var result = await scanner.ScanAsync();
            if (!string.IsNullOrEmpty(result))
            {
                lstEstoque = await BuscarEstoque(result);
            }
        }

        private async Task<ObservableCollection<EstoqueModel>> BuscarEstoque(string scdCodigo)
        {
            try
            {
                IsBusy = true;
                EstoqueLogic el = new EstoqueLogic();
                string estoque = await el.GetEstoque(scdCodigo);

                ObservableCollection<EstoqueModel> retorno = JsonConvert.DeserializeObject<ObservableCollection<EstoqueModel>>(estoque);

                return retorno;
            }
            finally
            {
                IsBusy = false;
            }
        }

        internal async Task BuscarProduto(string texto)
        {
            try
            {
                IsBusy = true;
                EstoqueLogic el = new EstoqueLogic();
                string estoque = await el.GetEstoque(texto);

                ObservableCollection<EstoqueModel> retorno = JsonConvert.DeserializeObject<ObservableCollection<EstoqueModel>>(estoque);

                lstEstoque = retorno;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
