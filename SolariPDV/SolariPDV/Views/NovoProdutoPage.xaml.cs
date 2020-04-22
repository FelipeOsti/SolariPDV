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
    public partial class NovoProdutoPage : ContentPage
    {
        NovoProdutoViewModel produtoViewModel;
        public NovoProdutoPage()
        {
            BindingContext = produtoViewModel = new NovoProdutoViewModel();
            InitializeComponent();
        }

        private void btConfirmar_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(produtoViewModel.scdCodigo))
            {
                DisplayAlert("Código", "Informe o Código do Material", "OK");
                return;
            }
            if (string.IsNullOrEmpty(produtoViewModel.sdsMaterial))
            {
                DisplayAlert("Código", "Informe a Descriçao do Material", "OK");
                return;
            }
            if (produtoViewModel.familiaSelecionada == null)
            {
                DisplayAlert("Familia", "Selecione a Familia", "Ok");
                return;
            }
            produtoViewModel.SalvarProdutoCommand.Execute(null);
        }
    }
}