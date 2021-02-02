using Newtonsoft.Json;
using SolariPDV.Models;
using SolariPDV.Models.Material;
using SolariPDV.Services;
using SolariPDV.Services.Material;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SolariPDV.ViewModels.Estoque
{
    public class MovimentacaoEstoqueViewModel : ViewModelBase
    {
        ICommand _GetMovimEstoqueCommand;
        public ICommand GetMovimEstoqueCommand { get { return _GetMovimEstoqueCommand; } set { SetValue(ref _GetMovimEstoqueCommand, value); } }

        ObservableCollection<MovimEstoqueModel> _lstMovEstoque;
        public ObservableCollection<MovimEstoqueModel> lstMovEstoque {get {return _lstMovEstoque;} set{SetValue(ref _lstMovEstoque, value); }}


        public MovimentacaoEstoqueViewModel()
        {
            GetMovimEstoqueCommand = new Command(GetMovimEstoque);
        }

        private async void GetMovimEstoque()
        {
            try
            {
                IsBusy = true;
                EstoqueLogic el = new EstoqueLogic();
                var json = await el.GetMovimeEstoque("");
                lstMovEstoque = JsonConvert.DeserializeObject<ObservableCollection<MovimEstoqueModel>>(json);
                IsBusy = false;
            }
            catch { }
        }

        public async Task BuscarProduto(string texto)
        {
            try
            {
                IsBusy = true;
                try
                {
                    EstoqueLogic el = new EstoqueLogic();
                    string estoque = await el.GetMovimeEstoque(texto);
                    var retorno = JsonConvert.DeserializeObject<ObservableCollection<MovimEstoqueModel>>(estoque);
                    lstMovEstoque = retorno;
                }
                catch{ }
            }          
            finally
            {
                IsBusy = false;
            }
        }
    }
}
