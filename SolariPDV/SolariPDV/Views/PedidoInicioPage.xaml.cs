using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SolariPDV.Logic;
using SolariPDV.Models;
using Newtonsoft.Json;
using SolariPDV.Services;
using SolariPDV.ViewModels;
using System.Collections.ObjectModel;

namespace SolariPDV.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PedidoInicioPage : ContentPage
    {
        public PedidoInicioPageViewModel pedidoInicioViewModel { get; set; }

        public PedidoInicioPage()
        {
            BindingContext = pedidoInicioViewModel = new PedidoInicioPageViewModel();
            InitializeComponent();
            IniciarInformacoes();
            Pedido.PedidoAtual = new PedidoModel();
            this.Title = "Novo Pedido";

            pedidoInicioViewModel.bboTemMesa = false;
            pedidoInicioViewModel.vlTotal = 0;
            pedidoInicioViewModel.txtItens = "Nenhum item";
        }

        private void IniciarInformacoes()
        {                     
            GetMesas();            
        }

        protected override void OnAppearing()
        {
            pedidoInicioViewModel.GetCategoriasCommand.Execute(null);
            base.OnAppearing();
            if (Pedido.PedidoAtual.Count > 0)
            {
                pedidoInicioViewModel.vlTotal = Pedido.PedidoAtual.VL_PEDIDO == null ? 0 : Pedido.PedidoAtual.VL_PEDIDO;
                pedidoInicioViewModel.txtItens = Pedido.PedidoAtual.Count > 1 ? Pedido.PedidoAtual.Count + " itens" : Pedido.PedidoAtual.Count + " item";
            }
            else 
            {
                pedidoInicioViewModel.vlTotal = 0;
                pedidoInicioViewModel.txtItens = "Nenhum Item";
            }
        }

        public PedidoInicioPage(PedidoSistemaModel _ped)
        {
            BindingContext = pedidoInicioViewModel = new PedidoInicioPageViewModel();
            InitializeComponent();
            CarregaPedido(_ped);
            IniciarInformacoes();            
        }

        private async void CarregaPedido(PedidoSistemaModel _ped)
        {
            await Pedido.IniciarPedido(_ped);
            if (_ped.ID_MESA > 0) pedidoInicioViewModel.bboTemMesa = true;

            pedidoInicioViewModel.txtItens = "Nenhum Item";
            pedidoInicioViewModel.vlTotal = _ped.VL_TOTAL;
            if (Pedido.PedidoAtual.Count > 0) pedidoInicioViewModel.txtItens = Pedido.PedidoAtual.Count > 1 ? Pedido.PedidoAtual.Count + " itens" : Pedido.PedidoAtual.Count + " item";

            if (_ped.ID_MESA > 0)
            {
                if (nrMesa.ItemsSource != null) nrMesa.SelectedItem = (nrMesa.ItemsSource as List<MesasModel>).FirstOrDefault(m => m.ID_MESA == _ped.ID_MESA);
            }
            else
            {
                nomeCliente.Text = _ped.DS_RAZAO;;
                nrTelefone.Text = Pedido.PedidoAtual.DS_TELEFONE;
            }

            ToolbarItem item = new ToolbarItem
            {
                Text = "Pagamento",
                IconImageSource = "payment.png",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            item.Command = new Command(FinalizaPedido);

            this.ToolbarItems.Add(item);
            this.Title = _ped.ID_PEDIDO > 0 ? "Pedido # " + _ped.ID_PEDIDO : "Novo Pedido";
        }

        private async void FinalizaPedido()
        {
            try
            {
                string action = await DisplayActionSheet("Forma de Pagamento", "Cancelar", null, "Dinheiro", "Crédito", "Débito");
                if (action == "Cancelar") return;

                string result = await DisplayPromptAsync("Valor Pago", "Valor do Pedido: "+Pedido.PedidoAtual.DS_VLPEDIDO, keyboard: Keyboard.Numeric, placeholder:""+ Pedido.PedidoAtual.VL_PEDIDO);
                if (string.IsNullOrEmpty(result)) return;

                Pedido.PedidoAtual.DS_FORMALIQ = action;
                Pedido.PedidoAtual.VL_PAGO = Double.Parse(result);

                await Navigation.PopToRootAsync();
                VerPedidosPage.current.pedidoViewModel.IsBusy = true;

                PedidoLogic pl = new PedidoLogic();
                await pl.FinalizarPedido(Pedido.PedidoAtual);
                
                VerPedidosPage.current.pedidoViewModel.GetPedidosCommand.Execute(null);
            }
            catch
            {
                await DisplayAlert("Ops", "Falha ao finalizar Pedido", "Tentar novamente");
            }
        }

        private async void GetMesas()
        {
            try
            {
                var mesaLogic = new MesaLogic();
                var lstMesas = await mesaLogic.getMesas();
                nrMesa.ItemsSource = lstMesas;
                if (Pedido.PedidoAtual.ID_MESA > 0) nrMesa.SelectedItem = (nrMesa.ItemsSource as ObservableCollection<MesasModel>).FirstOrDefault(m => m.ID_MESA == Pedido.PedidoAtual.ID_MESA);
            }
            catch { }
        }

        private void FABButton_Clicked(object sender, EventArgs e)
        {
            if (CardapioPage.current == null) { var cardapioPage = new CardapioPage(); }
            Navigation.PushAsync(new FinalizaPedidoPage());
        }

        private void lstView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var categ = (CardapioCateg)e.SelectedItem;
            if (categ == null) return;

            var cardapioPage = new CardapioPage();
            cardapioPage.SetCateg(categ);
            Navigation.PushAsync(cardapioPage);
            
            (sender as ListView).SelectedItem = null;
        }

        private void nomeCliente_Unfocused(object sender, FocusEventArgs e)
        {
            if(!String.IsNullOrEmpty(nomeCliente.Text))
            {
                nrMesa.SelectedItem = null;
                nrMesa.IsEnabled = false;
                Pedido.PedidoAtual.ID_MESA = 0;
                Pedido.PedidoAtual.DS_MESA = "";
            }
            else
            {
                nrMesa.IsEnabled = true;
            }

            Pedido.PedidoAtual.DS_CLIENTE = nomeCliente.Text;
        }

        private void nrMesa_Unfocused(object sender, FocusEventArgs e)
        {
            if (nrMesa.SelectedItem == null) return;

            var mesaModel = (nrMesa.SelectedItem as MesasModel);
            nomeCliente.Text = "";
            Pedido.PedidoAtual.DS_CLIENTE = "";
            Pedido.PedidoAtual.ID_MESA = mesaModel.ID_MESA;
            Pedido.PedidoAtual.DS_MESA = mesaModel.DS_MESA;
        }

        private void nrTelefone_Unfocused(object sender, FocusEventArgs e)
        {
            if (!String.IsNullOrEmpty(nrTelefone.Text))
            {
                nrMesa.SelectedItem = null;
                nrMesa.IsEnabled = false;
            }
            else if(!string.IsNullOrEmpty(nomeCliente.Text))
                nrMesa.IsEnabled = true;
        }
    }
}