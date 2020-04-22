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

namespace SolariPDV.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PedidoInicioPage : ContentPage
    {
        public static List<CardapioModel> Cardapio = new List<CardapioModel>();
        public static List<CardapioCateg> Categorias;
        PedidoInicioPageViewModel pedidoInicioViewModel;

        public PedidoInicioPage()
        {
            InitializeComponent();
            IniciarInformacoes();
            Pedido.PedidoAtual = new PedidoModel();
        }

        private void IniciarInformacoes()
        {
            BindingContext = pedidoInicioViewModel = new PedidoInicioPageViewModel();          
            GetMesas();
            GetCategorias();
        }

        public PedidoInicioPage(PedidoSistemaModel _ped)
        {
            InitializeComponent();
            IniciarInformacoes();

            Pedido.IniciarPedido(_ped);

            if (_ped.ID_MESA > 0)
            {
                nrMesa.SelectedItem = (nrMesa.ItemsSource as List<MesasModel>).FirstOrDefault(m => m.ID_MESA == _ped.ID_MESA);
                nomeCliente.IsEnabled = false;
                nrMesa.IsEnabled = true;
                nomeCliente.Text = "";
            }
            else
            {
                nrMesa.SelectedItem = null;
                nomeCliente.Text = _ped.DS_RAZAO;
                nrMesa.IsEnabled = false;
                nomeCliente.IsEnabled = true;
                nrTelefone.Text = Pedido.PedidoAtual.DS_TELEFONE;
            }

            ToolbarItem item = new ToolbarItem
            {
                Text = "Pagamento",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            item.Command = new Command(FinalizaPedido);

            // "this" refers to a Page object
            this.ToolbarItems.Add(item);
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
                DisplayAlert("Ops", "Falha ao finalizar Pedido", "Tentar novamente");
            }
        }

        private async void GetMesas()
        {
            var mesaLogic = new MesaLogic();
            var lstMesas = await mesaLogic.getMesas();
            nrMesa.ItemsSource = lstMesas;
            if (lstMesas?.Count > 0)
                pedidoInicioViewModel.bboTemMesa = true;
            else
                pedidoInicioViewModel.bboTemMesa = false;
        }

        private async void GetCategorias()
        {
            long ultimo = 0;
            Categorias = new List<CardapioCateg>();
            Cardapio = await RecuperaCardapioAsync();
            Cardapio.Sort((x, y) => x.ID_CATEGORIA.CompareTo(y.ID_CATEGORIA));
            foreach (var item in Cardapio)
            {
                if (ultimo != item.ID_CATEGORIA) Categorias.Add(new CardapioCateg() { ID_CATEGORIA = item.ID_CATEGORIA, DS_CATEGORIA = item.DS_CATEGORIA, FL_ADICIONAL = item.FL_ADICIONAL == "T", FL_ASSAR = item.FL_ASSAR, FL_PERMITEADICIONAL = item.FL_PERMITEADICIONAL == "T" });
                ultimo = item.ID_CATEGORIA;
            }
            lstView.ItemsSource = Categorias;
        }

        private async Task<List<CardapioModel>> RecuperaCardapioAsync()
        {
            var cardapioLogic = new CardapioLogic();
            var lista = await cardapioLogic.GetCardapio("");

            return JsonConvert.DeserializeObject<List<CardapioModel>>(lista);
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
            
            lstView.SelectedItem = null;
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