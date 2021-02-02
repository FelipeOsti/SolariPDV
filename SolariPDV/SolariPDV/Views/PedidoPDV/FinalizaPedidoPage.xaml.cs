using SolariPDV.Models;
using SolariPDV.Models.Comercial;
using SolariPDV.Services;
using SolariPDV.Services.Comercial;
using SolariPDV.ViewModels.PedidoPDV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SolariPDV.Views.PedidoPDV
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

            nvlTotalPedido.Text = Pedido.PedidoAtual.DS_VLPEDIDO;
        }

        private async void BtFinalizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                loadingIndicator.IsVisible = true;
                var pedidoL = new PedidoLogic();
                var pedido = await pedidoL.SalvarPedido(Pedido.PedidoAtual);

                if(Pedido.PedidoAtual == null || pedido == null)
                {
                    await DisplayAlert("Ops", "Algum problema com seu pedido!", "Tentar novamente!");
                }
                else
                {
                    VerPedidosPage.current.pedidoViewModel.GetPedidosCommand.Execute(null);
                    await Navigation.PopToRootAsync();
                    await DisplayAlert("Sucesso", "Pedido Salvo com Sucesso", "Ok");
                }
            }
            catch
            {
                await DisplayAlert("Falha", "Erro ao salvar pedido", "Ok"); 
            }
            finally
            {
                loadingIndicator.IsVisible = false;
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

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var item = (ItemPedidoModel)mi.CommandParameter;
            try
            {
                if (item.ID_ITEM > 0)
                {
                    var pedLogic = new PedidoLogic();
                    var retorno = await pedLogic.ExcluirItemPedido(item.ID_ITEM);
                    if (retorno.Replace("\"","") != "") throw new Exception("Falha ao excluir item - "+retorno);

                    await DisplayAlert("Avise a cozinha","Este item já estava em produção na cozinha. Avise seu cancelamento.","OK");
                }
                Pedido.PedidoAtual.Remove(item);

                nvlTotalPedido.Text = Pedido.PedidoAtual.DS_VLPEDIDO;                
            }
            catch { }
        }
    }
}