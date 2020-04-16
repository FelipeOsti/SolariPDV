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
    public partial class VerMesasPage : ContentPage
    {

        VerMesasViewModel verMesasViewModel;
        public VerMesasPage()
        {
            BindingContext = verMesasViewModel = new VerMesasViewModel();
            InitializeComponent();
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Navigation.PushAsync(new PedidoInicioPage());
        }
    }
}