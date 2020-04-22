using Newtonsoft.Json;
using SolariPDV.Logic;
using SolariPDV.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SolariPDV.ViewModels
{
    public class AdicionarItemViewModel : ViewModelBase
    {
        ObservableCollection<CaracModel> lstCarac;
        public ObservableCollection<CaracModel> LstCarac { get { return lstCarac; } set { SetValue(ref lstCarac, value); } }

        CardapioProd itemCardapio;
        public CardapioProd ItemCardapio { get { return itemCardapio; } set { SetValue(ref itemCardapio, value); } }


        bool _bboCarac;
        public bool bboCarac { get { return _bboCarac; } set { SetValue(ref _bboCarac, value); } }

        bool _bboAssar;
        public bool bboAssar { get { return _bboAssar; } set { SetValue(ref _bboAssar, value); } }

        public AdicionarItemViewModel(CardapioProd _item)
        {
            ItemCardapio = _item;
            bboAssar = Views.CardapioPage.current.CategoriaSelectionada.FL_ASSAR;
            GetCarac();
        }

        public void SetProduto(CardapioProd _item)
        {
            
        }

        private async void GetCarac()
        {
            var logic = new CaracLogic();
            var carac = await logic.GetCarac(ItemCardapio.ID_MATERIAL);

            LstCarac = JsonConvert.DeserializeObject<ObservableCollection<CaracModel>>(carac);

            bboCarac = false;
            if (LstCarac?.Count > 0) bboCarac = true;
        }
    }
    
}
