using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SolariPDV.Models;
using SolariPDV.ViewModels;
using SolariPDV.ViewModels.PopUps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SolariPDV.Views.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AcompanhamentoPedidoPoupUp : PopupPage
    {
        AcompanhamentoPedidoViewModel acompPedidoViewModel;
       
        public AcompanhamentoPedidoPoupUp(PedidoSistemaModel _pedido)
        {
            InitializeComponent();
            BindingContext = acompPedidoViewModel = new AcompanhamentoPedidoViewModel(_pedido);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                var botao = (sender as Button);
                var sdsAcao = "F";
                if (botao.Text == "Iniciar") sdsAcao = "I";

                var acompPage = AcompanharPedidoPage.current;
                acompPage.AcompPedidoViewModel.IniciarFinalizarPedido(acompPage.GetNumPedido(sender), sdsAcao);

                Button_Clicked(sender, e);
            }
            catch (Exception ex){
                DisplayAlert("Ops...", "Tente novamente mais tarde... " + ex.Message, "Ok");
            }
        }
    }
}