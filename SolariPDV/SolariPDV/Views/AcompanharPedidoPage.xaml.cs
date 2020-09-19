using Rg.Plugins.Popup.Services;
using SolariPDV.Models;
using SolariPDV.ViewModels;
using SolariPDV.Views.PopUps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace SolariPDV.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AcompanharPedidoPage : ContentPage
    {
        public AcompanharPedidoViewModel AcompPedidoViewModel;
        public static AcompanharPedidoPage current;
        
        public AcompanharPedidoPage()
        {
            BindingContext = AcompPedidoViewModel = new AcompanharPedidoViewModel();
            current = this;
            InitializeComponent();

            var nnrSpan = 2;
            if (Device.Idiom == TargetIdiom.Tablet) nnrSpan = 3;
            collectionPedidos.ItemsLayout = new GridItemsLayout(nnrSpan, ItemsLayoutOrientation.Horizontal);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AcompPedidoViewModel.GetPedidosCommand.Execute(null);
            AcompPedidoViewModel.timer.Enabled = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            AcompPedidoViewModel.timer.Enabled = false;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var numPedido = GetNumPedido(sender);
                if (numPedido == 0) return;
                var Pedido = AcompPedidoViewModel.LstPedidos.FirstOrDefault(ped => ped.ID_PEDIDO == numPedido);
                PopupNavigation.Instance.PushAsync(new AcompanhamentoPedidoPoupUp(Pedido));
            }
            catch { }
        }

        public long GetNumPedido(object sender)
        {
            var lstFields = (((sender as Button).Parent as StackLayout).Parent as Grid).Children;
            var numPedido = (((lstFields[0] as StackLayout).Children[1] as StackLayout).Children[2] as Label).Text;
            return long.Parse(numPedido);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                var botao = (sender as Button);
                var sdsAcao = "F";
                if (botao.Text == "Iniciar") sdsAcao = "I"; 
                AcompPedidoViewModel.IniciarFinalizarPedido(GetNumPedido(sender), sdsAcao);
                botao.Text = "Finalizar" ;
            }
            catch (Exception ex){
                DisplayAlert("Ops...", "Tente novamente mais tarde... "+ex.Message, "Ok");
            }
        }
    }
}