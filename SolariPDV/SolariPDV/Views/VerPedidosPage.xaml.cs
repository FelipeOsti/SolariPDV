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
    public partial class VerPedidosPage : ContentPage
    {
        public VerPedidosViewModel pedidoViewModel;

        public static VerPedidosPage current;
        public VerPedidosPage()
        {
            BindingContext = pedidoViewModel = new VerPedidosViewModel();
            InitializeComponent();            
            current = this;
        }

        protected override void OnAppearing()
        {            
            base.OnAppearing();
            //pedidoViewModel.GetPedidosCommand.Execute(null);
        }

        private void lstViewPedidos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if ((sender as ListView).SelectedItem == null) return;
            if (e.Item == null) return;

            var pedido = (PedidoSistemaModel)e.Item;

            Navigation.PushAsync(new PedidoInicioPage(pedido));

            (sender as ListView).SelectedItem = null;
        }

        private void FABButtonMesas_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VerMesasPage());
        }

        private void FABButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PedidoInicioPage());
        }

        private void btBusca_Clicked(object sender, EventArgs e)
        {
            pedidoViewModel.bboSearch = !pedidoViewModel.bboSearch;
        }

        private void btBuscar_Clicked(object sender, EventArgs e)
        {
            pedidoViewModel.GetPedidosCommand.Execute(null);
            pedidoViewModel.bboSearch = false;
        }

        private void FABButtonCozinha_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AcompanharPedidoPage(),true);
        }
    }
}