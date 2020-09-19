using SolariPDV.GradientView;
using SolariPDV.Models;
using SolariPDV.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SolariPDV.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();
            GetDadosInicio();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Inicio, Title="Inicio"},
                new HomeMenuItem {Id = MenuItemType.Estoque, Title="Consulta de Estoque" },
                new HomeMenuItem {Id = MenuItemType.PedidoPDV, Title="Pedido PDV" },
                new HomeMenuItem {Id = MenuItemType.PedidoComercial, Title="Pedido Comercial" },
                new HomeMenuItem {Id = MenuItemType.AcompanharPedido, Title="Acompanhar Pedido" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }

        private async void GetDadosInicio()
        {
            try
            {
                var menuLogic = new MenuLogic();
                var menuModel = await menuLogic.GetDadosMenu();

                pickerEstab.ItemsSource = menuModel.lstEstabelecimento;
                pickerEstab.SelectedItem = menuModel.lstEstabelecimento[0];
                nomeUsuario.Text = menuModel.DS_USUARIO;
            }
            catch
            {
                await DisplayAlert("Ops", "Falha ao carregar dados do usuário", "ok");
            }
        }

        private void pickerEstab_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            if (picker == null) return;
            var estab = (picker.SelectedItem as EstabelecimentoModel);

            App.current.EstabSelected = estab;

            InicioPage.current.inicioViewModel.GetDashBoardCommand.Execute(null);
        }

        private void btSair_Clicked(object sender, EventArgs e)
        {
            App.current.limparLogin();
            App.current.MainPage = new NavigationGradient(new LoginPage());
        }
    }
}