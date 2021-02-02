using SolariPDV.Models;
using SolariPDV.Models.Material;
using SolariPDV.ViewModels;
using SolariPDV.ViewModels.Estoque;
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
    public partial class FamiliaEstoquePage : ContentPage
    {

        FamiliaEstoqueViewModel familiaViewModel;
        public FamiliaEstoquePage()
        {
            InitializeComponent();
            BindingContext = familiaViewModel = new FamiliaEstoqueViewModel();            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            familiaViewModel.GetFamiliasCommand.Execute(null);
        }

        private void listViewFamilia_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            familiaViewModel.EditarFamilia((e.SelectedItem as FamiliaModel).ID_FAMILIA, (e.SelectedItem as FamiliaModel).DS_FAMILIA);
        }

        private void FABButton_Clicked(object sender, EventArgs e)
        {
            familiaViewModel.AdicionarFamilia();
        }
    }
}