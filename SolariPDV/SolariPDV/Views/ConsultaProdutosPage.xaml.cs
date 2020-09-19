using SolariPDV.Models;
using SolariPDV.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SolariPDV.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaProdutosPage : ContentPage
    {
        public static ConsultaProdutosPage ConsultaProdutoPageCurrent;
        ConsultaProdutosViewModel consultaViewModel;
        public static MaterialModel produtoSelecionado = null;
        public ConsultaProdutosPage()
        {
            ConsultaProdutoPageCurrent = this;
            BindingContext = consultaViewModel = new ConsultaProdutosViewModel();
            InitializeComponent();
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
            consultaViewModel.GetProdutoCommand.Execute(null);
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
            consultaViewModel.GetProdutoCommand.Execute(filtro);
        }
    }
}