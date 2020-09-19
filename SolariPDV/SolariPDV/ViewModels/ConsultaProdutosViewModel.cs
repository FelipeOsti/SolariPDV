using Newtonsoft.Json;
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
    public class ConsultaProdutosViewModel : ViewModelBase
    {
        ObservableCollection<MaterialModel> _lstMateriais;
        public ObservableCollection<MaterialModel> LstMateriais { get { return _lstMateriais; } set { SetValue(ref _lstMateriais, value); } }

        public ConsultaProdutosViewModel()
        {
            GetProdutoCommand = new Command(GetProdutos);
        }

        private async void GetProdutos(object filtro)
        {
            try
            {
                IsBusy = true;
                var matL = new MaterialLogic();
                var json = await matL.GetMateriais((string)filtro);
                LstMateriais = JsonConvert.DeserializeObject<ObservableCollection<MaterialModel>>(json);
                IsBusy = false;
            }
            catch { }
        }

        public ICommand GetProdutoCommand;
    }
}
