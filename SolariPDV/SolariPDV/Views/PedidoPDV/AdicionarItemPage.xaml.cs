using SolariPDV.Models;
using SolariPDV.Models.Comercial;
using SolariPDV.Models.Material;
using SolariPDV.ViewModels;
using SolariPDV.ViewModels.PedidoPDV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SolariPDV.Views.PedidoPDV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdicionarItemPage : ContentPage
    {
        AdicionarItemViewModel addItemViewModel;

        public AdicionarItemPage(CardapioProd item)
        {
            BindingContext = addItemViewModel = new AdicionarItemViewModel(item);
            InitializeComponent();
            labelQtde.Text = "1";
        }

        private async void btAdicionar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (lstViewTamanho.SelectedItem == null)
                {
                    if ((lstViewTamanho.ItemsSource as List<TamanhoProd>).Count > 1)
                    {
                        await DisplayAlert("Selecione o Tamanho", "Favor selecionar o tamanho do produto", "Ok");
                        return;
                    }
                    else
                    {
                        lstViewTamanho.SelectedItem = (lstViewTamanho.ItemsSource as List<TamanhoProd>)[0];
                    }
                }

                var tamanho = ((TamanhoProd)lstViewTamanho.SelectedItem);
                double nvlAdicional = 0;
                string sdsMaterial = addItemViewModel.ItemCardapio.DS_MATERIAL;

                CaracModel carac = null;
                if (lstViewCarac.ItemsSource != null)
                {
                    if (lstViewCarac.SelectedItem == null)
                    {
                        lstViewCarac.SelectedItem = addItemViewModel.LstCarac.FirstOrDefault(c => c.QT_QUANTIDADE == 1);
                    }

                    carac = ((CaracModel)lstViewCarac.SelectedItem);
                    CardapioPageViewModel.nqtCarac = carac.QT_QUANTIDADE;
                    if (carac.QT_QUANTIDADE > 1)
                    {
                        CardapioPage.current.MostrarLabel();
                    }
                    else
                    {
                        if (carac.FL_CARACMATNOME == "T") sdsMaterial = sdsMaterial + " " + carac.DS_CARACMAT;
                        sdsMaterial = CardapioPage.current.CategoriaSelectionada.DS_CATEGORIA + " " + sdsMaterial + " " + tamanho.DS_TAMANHO;
                        nvlAdicional = carac.VL_ADICIONAL;
                    }                    
                }
                else
                {
                    CardapioPageViewModel.nqtCarac = 1;
                    sdsMaterial = CardapioPage.current.CategoriaSelectionada.DS_CATEGORIA + " " + sdsMaterial + " " + tamanho.DS_TAMANHO;
                }

                double? nvlDesconto = 0;
                if (addItemViewModel.vlDesconto > 0) nvlDesconto = addItemViewModel.vlDesconto;
                if (addItemViewModel.txDesconto > 0) nvlDesconto = (qtdItem.Value * tamanho.VL_UNITARIO) * addItemViewModel.txDesconto / 100;

                Pedido.PedidoAtual.Add(new ItemPedidoModel()
                {
                    DS_MATERIAL = sdsMaterial,
                    BO_ASSAR = bboAssar.IsToggled, 
                    DS_TAMANHO = tamanho.DS_TAMANHO,
                    ID_ITEM = 0,
                    ID_MATERIAL = addItemViewModel.ItemCardapio.ID_MATERIAL,
                    ID_TAMANHO = tamanho.ID_TAMANHO,
                    ID_CARACMAT = carac != null ? carac.ID_CARACMAT : 0, 
                    QT_CARAC = carac != null ? carac.QT_QUANTIDADE : 1,
                    VL_ADICIONALCARAC = carac != null ? carac.VL_ADICIONAL : 0,
                    QT_PEDIDO = qtdItem.Value,
                    VL_UNITARIO = tamanho.VL_UNITARIO + nvlAdicional,
                    FL_PERMITEADICIONAL = CardapioPage.current.CategoriaSelectionada.FL_PERMITEADICIONAL,
                    VL_DESCONTO = nvlDesconto
                });
                Pedido.PedidoAtual[Pedido.PedidoAtual.Count - 1].AdicionarFicha(addItemViewModel.ItemCardapio.lstFicha);

                if (bboAssar.IsToggled)
                {
                    if (CardapioPageViewModel.nqtCarac == 1) Pedido.IncluirItemFilho("I", new ItemPedidoModel()
                    {
                        ID_MATERIAL = 0,
                        VL_UNITARIO = 5,
                        DS_MATERIAL = "Assar",
                        ID_ITEM = 0,
                        QT_PEDIDO = 1,
                        ID_TAMANHO = 0,
                        BO_ASSAR = false,
                        BO_RELACIONADO = true
                    });
                }

                await Navigation.PopAsync();
            }
            catch
            {

            }
        }

        private void qtdItem_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            labelQtde.Text = ""+e.NewValue;
        }
    }
}