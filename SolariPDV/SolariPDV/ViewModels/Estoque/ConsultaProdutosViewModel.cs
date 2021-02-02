using Newtonsoft.Json;
using SolariPDV.Models;
using SolariPDV.Models.Material;
using SolariPDV.Services;
using SolariPDV.Services.Material;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SolariPDV.ViewModels.Estoque
{
    public class ConsultaProdutosViewModel : ViewModelBase
    {
        ObservableCollection<MaterialModel> _lstMateriais;
        public ObservableCollection<MaterialModel> LstMateriais { get { return _lstMateriais; } set { SetValue(ref _lstMateriais, value); } }


        ICommand _GetProdutoCommand;
        public ICommand GetProdutoCommand { get { return _GetProdutoCommand; } set { SetValue(ref _GetProdutoCommand, value); } }

        public ConsultaProdutosViewModel()
        {
            IsBusy = false;
            GetProdutoCommand = new Command(RecuperarTodosProdutos);
        }

        private void RecuperarTodosProdutos()
        {
            GetProdutos(null);
        }

        public async void GetProdutos(object filtro)
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var matL = new MaterialLogic();
                var json = await matL.GetMateriais((string)filtro);
                LstMateriais = JsonConvert.DeserializeObject<ObservableCollection<MaterialModel>>(json);
                
            }
            catch { }
            finally { IsBusy = false; }
        }
    }
}
