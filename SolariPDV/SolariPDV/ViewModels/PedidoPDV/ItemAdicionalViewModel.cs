using SolariPDV.Models;
using SolariPDV.Models.Material;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SolariPDV.ViewModels.PedidoPDV
{
    public class ItemAdicionalViewModel : ViewModelBase
    {
        ObservableCollection<FichaModel> _lstFicha;
        public ObservableCollection<FichaModel> LstFicha { get { return _lstFicha; } set { SetValue(ref _lstFicha, value); } }

        ObservableCollection<CardapioProd> _lstAdicionais;
        public ObservableCollection<CardapioProd> LstAdicionais { get { return _lstAdicionais; } set {SetValue(ref _lstAdicionais, value); } }

        public ObservableCollection<FichaModel> LstFichaOrifinal { get; internal set; }
        public ObservableCollection<CardapioProd> LstAdicionaisOriginal { get; internal set; }

        public ItemAdicionalViewModel()
        {
            
        }

        internal ObservableCollection<FichaModel> GetSearchResultsFicha(string text)
        {
            return new ObservableCollection<FichaModel>(LstFichaOrifinal.Where(f => f.DS_MATERIAL.Contains(text)));
        }

        internal ObservableCollection<CardapioProd> GetSearchResultsAdd(string text)
        {
            return new ObservableCollection<CardapioProd>(LstAdicionaisOriginal.Where(a => a.DS_MATERIAL.Contains(text)));
        }
    }
}
