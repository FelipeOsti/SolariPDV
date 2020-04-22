using SolariPDV.ViewModels;
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
    public partial class EstoqueConsultaPage : ContentPage
    {
        EstoqueViewModel estoqueViewModel;
        private bool bboBarCode;
        private bool bboShowOneTime = false;
        public EstoqueConsultaPage(bool _bboBarCode)
        {
            BindingContext = estoqueViewModel = new EstoqueViewModel();
            bboBarCode = _bboBarCode;
            InitializeComponent();            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (bboBarCode && !bboShowOneTime)
            {
                bboShowOneTime = true;
                estoqueViewModel.EscanearCodigoBarras();
            }
            else bboShowOneTime = false;
            //estoqueViewModel.GetEstoqueCommand.Execute(null);
        }

        private async void ProdutosSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            if (searchBar == null) return;
            var texto = searchBar.Text;
            await estoqueViewModel.BuscarProduto(texto);
        }

        private void FABButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NovoProdutoPage());
        }

        private void listViewEstoque_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
        }
    }
}