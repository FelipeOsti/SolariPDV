using Rg.Plugins.Popup.Services;
using SolariPDV.Models;
using SolariPDV.ViewModels;
using SolariPDV.Views.PopUps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SolariPDV.Views.Estoque
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovimentacaoEstoquePage : ContentPage
    {
        public MovimentacaoEstoqueViewModel movViewModel;

        MovimEstoqueModel estoqueSelected;

        public static MovimentacaoEstoquePage current;

        public MovimentacaoEstoquePage()
        {
            BindingContext = movViewModel = new MovimentacaoEstoqueViewModel();
            InitializeComponent();
            current = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            movViewModel.GetMovimEstoqueCommand.Execute(null);
        }

        private void listViewMovEstoque_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if((sender as ListView).SelectedItem != null) estoqueSelected = ((sender as ListView).SelectedItem as MovimEstoqueModel);
            (sender as ListView).SelectedItem = null;
        }

        private void FABButton_Clicked(object sender, EventArgs e)
        {
            if(estoqueSelected == null)
            {
                DisplayAlert("Selecione", "Selecione um produto antes de movimentar", "Ok");
                return;
            }
            PopupNavigation.Instance.PushAsync(new NovaMovimPopUp(estoqueSelected.ID_MATERIAL, estoqueSelected.DS_MATERIAL));
        }

        private async void ProdutosSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            if (searchBar == null) return;
            var texto = searchBar.Text;
            await movViewModel.BuscarProduto(texto);
        }
    }
}