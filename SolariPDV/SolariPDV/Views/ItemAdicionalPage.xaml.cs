using SolariPDV.Models;
using SolariPDV.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SolariPDV.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemAdicionalPage : ContentPage
    {
        ItemAdicionalViewModel itemViewModel;

        ItemPedidoModel itemPedido;

        public ItemAdicionalPage(ItemPedidoModel item)
        {
            BindingContext = itemViewModel = new ItemAdicionalViewModel();
            itemPedido = item;
            InitializeComponent();
            itemViewModel.LstFicha = item.lstFicha;
            itemViewModel.LstFichaOrifinal = item.lstFicha;
            itemViewModel.LstAdicionais = new ObservableCollection<CardapioProd>(CardapioPage.current.cardapioViewModel.GetAdicional());
            itemViewModel.LstAdicionaisOriginal = new ObservableCollection<CardapioProd>(CardapioPage.current.cardapioViewModel.GetAdicional());
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            SearchBar searchBar = (SearchBar)sender;
            itemViewModel.LstFicha = itemViewModel.GetSearchResultsFicha(searchBar.Text);
            itemViewModel.LstAdicionais = itemViewModel.GetSearchResultsAdd(searchBar.Text);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var index = 0;
            if (itemPedido != null)
            {
                index = Pedido.PedidoAtual.IndexOf(itemPedido);
                index++;
            }

            if (itemViewModel.LstAdicionais != null)
            {
                foreach (var add in itemViewModel.LstAdicionais)
                {
                    if (add.BO_SELECIONA)
                    {
                        Pedido.IncluirItemFilho("I", new ItemPedidoModel()
                        {
                            BO_RELACIONADO = true,
                            ID_ITEM = 0,
                            ID_MATERIAL = add.ID_MATERIAL,
                            DS_MATERIAL = add.DS_MATERIAL,
                            VL_UNITARIO = add.VL_UNITARIO,
                            QT_PEDIDO = 1
                        }, index);

                        add.BO_SELECIONA = false;
                    }
                }
            }

            if (itemViewModel.LstFicha != null)
            {
                foreach (var ficha in itemViewModel.LstFicha)
                {
                    if (ficha.BO_SELECIONA)
                    {
                        Pedido.IncluirItemFilho("R", new ItemPedidoModel()
                        {
                            BO_RELACIONADO = true,
                            ID_ITEM = 0,
                            ID_MATERIAL = ficha.ID_MATERIAL,
                            DS_MATERIAL = ficha.DS_MATERIAL,
                            VL_UNITARIO = 0,
                            QT_PEDIDO = 1
                        }, index);

                        ficha.BO_SELECIONA = false;
                    }
                }
            }

            Navigation.PopAsync();
        }
    }
}