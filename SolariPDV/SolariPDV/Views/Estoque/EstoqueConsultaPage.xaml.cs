using SolariPDV.Models;
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
        public static EstoqueConsultaPage EstoqueConsultaPageCurrent;
        EstoqueViewModel estoqueViewModel;
        public static EstoqueModel estoqueSelecionado = null;

        private bool bboBarCode;
        private bool bboShowOneTime = false;
        public EstoqueConsultaPage(bool _bboBarCode)
        {
            EstoqueConsultaPageCurrent = this;
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
            if ((sender as ListView).SelectedItem == null) return;
            estoqueSelecionado = (EstoqueModel)e.SelectedItem;
            //(sender as ListView).SelectedItem = null;
        }

        internal MaterialModel GetMaterialSelecionado()
        {
            try
            {
                return new MaterialModel()
                {
                    ID_MATERIAL = estoqueSelecionado.ID_MATERIAL,
                    DS_MATERIAL = estoqueSelecionado.DS_MATERIAL
                };
            }
            catch { return null; }
        }
    }
}