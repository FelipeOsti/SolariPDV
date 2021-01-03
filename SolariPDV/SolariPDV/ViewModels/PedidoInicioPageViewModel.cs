using Newtonsoft.Json;
using SolariPDV.Logic;
using SolariPDV.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SolariPDV.ViewModels
{
    public class PedidoInicioPageViewModel : ViewModelBase
    {
        private double? _vlTotal;
        public double? vlTotal { get { return _vlTotal; } set { SetValue(ref _vlTotal, value); } }

        ICommand _getCategoriasCommand;
        public ICommand GetCategoriasCommand { get { return _getCategoriasCommand; } set { SetValue(ref _getCategoriasCommand, value); } }
        
        public PedidoInicioPageViewModel()
        {
            LstCategorias = new ObservableCollection<CardapioCateg>();
            GetCategoriasCommand = new Command(GetCategorias);
        }

        internal async void GetCategorias()
        {
            try
            {
                IsBusy = true;
                long ultimo = 0;
                LstCategorias.Clear();

                var Cardapio = await RecuperaCardapioAsync();
                Cardapio.Sort((x, y) => x.ID_CATEGORIA.CompareTo(y.ID_CATEGORIA));
                foreach (var item in Cardapio)
                {
                    if (ultimo != item.ID_CATEGORIA) LstCategorias.Add(new CardapioCateg() { ID_CATEGORIA = item.ID_CATEGORIA, DS_CATEGORIA = item.DS_CATEGORIA, FL_ADICIONAL = item.FL_ADICIONAL == "T", FL_ASSAR = item.FL_ASSAR, FL_PERMITEADICIONAL = item.FL_PERMITEADICIONAL == "T" });
                    ultimo = item.ID_CATEGORIA;
                }
                
            }
            finally { IsBusy = false; }
        }

        internal async Task<List<CardapioModel>> RecuperaCardapioAsync()
        {
            var cardapioLogic = new CardapioLogic();
            var lista = await cardapioLogic.GetCardapio("");

            return JsonConvert.DeserializeObject<List<CardapioModel>>(lista);
        }

        ObservableCollection<CardapioCateg> _lstCategorias;
        public ObservableCollection<CardapioCateg> LstCategorias { get { return _lstCategorias; } set { SetValue(ref _lstCategorias, value); } }

        bool _bboTemMesa;

        string _txtItens;
        public string txtItens { get { return _txtItens; } set { SetValue(ref _txtItens, value); } }

        public bool bboTemMesa { get { return _bboTemMesa; } set { SetValue(ref _bboTemMesa, value); } }
    }
}
