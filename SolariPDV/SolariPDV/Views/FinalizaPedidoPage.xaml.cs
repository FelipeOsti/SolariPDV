using SolariPDV.Models;
using SolariPDV.Services;
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
    public partial class FinalizaPedidoPage : ContentPage
    {
        public FinalizaPedidoPage()
        {
            InitializeComponent();
            Title = "Finalização do Pedido";
            lstViewPedido.ItemsSource = Pedido.PedidoAtual;
            btFinalizar.Clicked += BtFinalizar_Clicked;
        }

        private async void BtFinalizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var pedidoL = new PedidoLogic();
                var pedido = await pedidoL.SalvarPedido(Pedido.PedidoAtual);

                if(Pedido.PedidoAtual == null)
                {
                    await DisplayAlert("Ops", "Algum problema com seu pedido!", "Tentar novamente!");
                }
                else
                {
                    VerPedidosPage.current.pedidoViewModel.BuscarInformacoesIniciais();
                    await Navigation.PopToRootAsync();
                    await DisplayAlert("Sucesso", "Pedido Salvo com Sucesso", "Ok");
                }
            }
            catch
            {
                await DisplayAlert("Falha", "Erro ao salvar pedido", "Ok"); 
            }
        }

        private void lstViewPedido_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;

            if (e.Item == null) return;
            var item = (e.Item as ItemPedidoModel);
            if (item.BO_RELACIONADO) return;
            if (!item.FL_PERMITEADICIONAL) return;

            Navigation.PushAsync(new ItemAdicionalPage(item));          
        }
    }
}