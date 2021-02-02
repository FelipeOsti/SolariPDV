using dotMorten.Xamarin.Forms;
using SolariPDV.Models.Comercial;
using SolariPDV.Models.Financeiro;
using SolariPDV.ViewModels.PedidoComercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SolariPDV.Views.PedidoComercial
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PedidoComercialPage : ContentPage
    {
        PedidoComercialViewModel pedidoViewModel;
        public PedidoComercialPage()
        {
            InitializeComponent();
            BindingContext = pedidoViewModel = new PedidoComercialViewModel();

            IniciarPedido();
        }

        private void IniciarPedido()
        {
            //pedidoViewModel.IniciarPedido();
        }

        public PedidoComercialPage(PedidoSistemaModel _pedido)
        {
            InitializeComponent();
            BindingContext = pedidoViewModel = new PedidoComercialViewModel();
        }

        private void AutoSuggestBox_TextChanged(object sender, dotMorten.Xamarin.Forms.AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                // Definir o ItemsSource como seu conjunto de dados filtrado
                // sender.ItemsSource = dataset; 
                FiltrarClientes((sender as AutoSuggestBox).Text, sender);
            }
        }

        private void AutoSuggestBox_QuerySubmitted(object sender, dotMorten.Xamarin.Forms.AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                // O usuário selecionou um item da lista de sugestões, execute uma ação sobre ele aqui. 
                if (args.ChosenSuggestion != null)
                    pedidoViewModel.ClienteSelected = (ClientesModel)args.ChosenSuggestion;
                else
                    pedidoViewModel.ClienteSelected = null;
            }
            else
            {
                // O usuário pressiona Enter na caixa de pesquisa. Use args.QueryText para determinar o que fazer.
                FiltrarClientes(args.QueryText, sender);
            }
        }

        private void AutoSuggestBox_SuggestionChosen(object sender, dotMorten.Xamarin.Forms.AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            // Definir sender.Text. Você pode usar args.SelectedItem para construir sua string de texto.
            if (args.SelectedItem != null)
                (sender as AutoSuggestBox).Text = ((ClientesModel)args.SelectedItem).DS_RAZAO+" - "+ ((ClientesModel)args.SelectedItem).DS_CIDADE;
        }

        public void FiltrarClientes(string sdsFiltro, object sender)
        {
            if (pedidoViewModel.LstClientes != null)
            {
                var clientes = pedidoViewModel.LstClientes.Where(c => c.DS_RAZAO.ToUpper().Contains(sdsFiltro.ToUpper())).ToList();
                (sender as AutoSuggestBox).ItemsSource = clientes;
            }
        }

        private void AutoSuggestBox_TextChanged_1(object sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                // Definir o ItemsSource como seu conjunto de dados filtrado
                // sender.ItemsSource = dataset; 
                FiltrarCondicoes((sender as AutoSuggestBox).Text, sender);
            }
        }

        private void AutoSuggestBox_QuerySubmitted_1(object sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if(args.ChosenSuggestion != null)
            {
                // O usuário selecionou um item da lista de sugestões, execute uma ação sobre ele aqui. 
                if (args.ChosenSuggestion != null)
                    pedidoViewModel.CondicaoSelected = (CondicaoRecebimentoModel)args.ChosenSuggestion;
                else
                    pedidoViewModel.CondicaoSelected = null;
            }
            else
            {
                // O usuário pressiona Enter na caixa de pesquisa. Use args.QueryText para determinar o que fazer.
                FiltrarCondicoes(args.QueryText, sender);
            }
        }

        private void FiltrarCondicoes(string queryText, object sender)
        {
            if (pedidoViewModel.LstCondicao != null)
            {
                var condicao = pedidoViewModel.LstCondicao.Where(c => c.DS_CONDRECEB.ToUpper().Contains(queryText.ToUpper())).ToList();
                (sender as AutoSuggestBox).ItemsSource = condicao;
            }
        }

        private void AutoSuggestBox_SuggestionChosen_1(object sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem != null)
                (sender as AutoSuggestBox).Text = ((CondicaoRecebimentoModel)args.SelectedItem).DS_CONDRECEB + " - " + ((CondicaoRecebimentoModel)args.SelectedItem).DS_FORMALIQ;
        }
    }
}