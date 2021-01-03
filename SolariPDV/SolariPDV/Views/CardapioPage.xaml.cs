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
    public partial class CardapioPage : ContentPage
    {
        public CardapioPageViewModel cardapioViewModel;
        public static CardapioPage current;


        public CardapioCateg CategoriaSelectionada;

        public CardapioPage()
        {
            BindingContext = cardapioViewModel = new CardapioPageViewModel();
            InitializeComponent();
            current = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Pedido.PedidoAtual.Count > 0)
            {
                cardapioViewModel.vlTotal = Pedido.PedidoAtual.VL_PEDIDO == null ? 0 : Pedido.PedidoAtual.VL_PEDIDO;
                cardapioViewModel.txtItens = Pedido.PedidoAtual.Count > 1 ? Pedido.PedidoAtual.Count + " itens" : Pedido.PedidoAtual.Count + " item";
            }
            else
            {
                cardapioViewModel.vlTotal = 0;
                cardapioViewModel.txtItens = "Nenhum Item";
            }
        }

        private async void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            if (searchBar == null) return;
            var texto = searchBar.Text;
            cardapioViewModel.FiltrarProduto(texto);
        }

        internal void SetCateg(CardapioCateg categ)
        {
            CategoriaSelectionada = categ;
            cardapioViewModel.SetCategoria(categ);
        }

        private void lstViewCardapio_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            lstViewCardapio.SelectedItem = null;
            if (e.Item == null) return;

            if (CardapioPageViewModel.nqtCarac > 1)
            {
                CardapioPageViewModel.nqtCarac -= 1;
                if (CardapioPageViewModel.nqtCarac == 1) lblMetade.IsVisible = false;

                var tamanho = ((CardapioProd)e.Item).FirstOrDefault(t => t.ID_TAMANHO == Pedido.PedidoAtual[Pedido.PedidoAtual.Count - 1].ID_TAMANHO);

                Pedido.PedidoAtual[Pedido.PedidoAtual.Count - 1].DS_MATERIAL += " / " + ((CardapioProd)e.Item).DS_MATERIAL + " "+ tamanho.DS_TAMANHO;
                Pedido.PedidoAtual[Pedido.PedidoAtual.Count - 1].DS_MATERIAL = CategoriaSelectionada.DS_CATEGORIA + " " + Pedido.PedidoAtual[Pedido.PedidoAtual.Count - 1].DS_MATERIAL;
                Pedido.PedidoAtual[Pedido.PedidoAtual.Count - 1].LstPedMater.Add(((CardapioProd)e.Item).ID_MATERIAL);

                Pedido.PedidoAtual[Pedido.PedidoAtual.Count - 1].AdicionarFicha(((CardapioProd)e.Item).lstFicha);

                var unitar = Pedido.PedidoAtual[Pedido.PedidoAtual.Count - 1].VL_UNITARIO + tamanho.VL_UNITARIO;
                unitar = (unitar / 2) + 2;
                Pedido.PedidoAtual[Pedido.PedidoAtual.Count - 1].VL_UNITARIO = unitar;

                if (Pedido.PedidoAtual[Pedido.PedidoAtual.Count - 1].BO_ASSAR)
                {
                    Pedido.IncluirItemFilho("I", new ItemPedidoModel()
                    {
                        ID_MATERIAL = 0,
                        VL_UNITARIO = 5,
                        DS_MATERIAL = "Assar",
                        ID_ITEM = 0,
                        QT_PEDIDO = 1,
                        ID_TAMANHO = 0,
                        BO_ASSAR = false,
                        BO_RELACIONADO = true,
                    });
                }
            }
            else
            {
                Navigation.PushAsync(new AdicionarItemPage((CardapioProd)e.Item), true);
            }
        }

        internal void MostrarLabel()
        {
            lblMetade.IsVisible = true;
        }

        private void FABButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FinalizaPedidoPage());
        }
    }
}