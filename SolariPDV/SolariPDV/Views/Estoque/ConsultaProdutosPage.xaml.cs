using SolariPDV.Models;
using SolariPDV.Models.Material;
using SolariPDV.ViewModels;
using SolariPDV.ViewModels.Estoque;
using SolariPDV.Views.PedidoPDV;
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
    public partial class ConsultaProdutosPage : ContentPage
    {
        public static ConsultaProdutosPage ConsultaProdutoPageCurrent;

        ConsultaProdutosViewModel consultaViewModel;
        public static MaterialModel produtoSelecionado = null;
        public ConsultaProdutosPage()
        {
            InitializeComponent();
            ConsultaProdutoPageCurrent = this;
            this.BindingContext = consultaViewModel = new ConsultaProdutosViewModel();            
        }

        public MaterialModel GetMaterialSelecionado()
        {
            try
            {
                return produtoSelecionado;
            }
            catch { return null; }
        }

        protected override void OnAppearing()
        {            
            base.OnAppearing();
            CarregarProdutos();
        }

        private void CarregarProdutos()
        {
            consultaViewModel.GetProdutos(MaterialSearchBar.Text);
            consultaViewModel.IsBusy = false;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if ((sender as ListView).SelectedItem == null) return;
            if (e.Item == null) return;

            var material = (MaterialModel)e.Item;

            Navigation.PushAsync(new NovoProdutoPage(material));

            produtoSelecionado = material;
        }

        private void FABButtonAdd_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NovoProdutoPage());
        }

        private void MaterialSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            if (searchBar == null) return;
            var filtro = searchBar.Text;
            consultaViewModel.GetProdutos(filtro);
        }
    }
}