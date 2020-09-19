using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SolariPDV.Models;
using SolariPDV.GradientView;

namespace SolariPDV.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;
        }


        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Inicio:
                        MenuPages.Add(id, new NavigationGradient(new InicioPage()) { BarTextColor = Color.White });
                        break;
                    case (int)MenuItemType.Estoque:
                        MenuPages.Add(id, new NavigationGradient(new EstoquePage()) { BarTextColor = Color.White});
                        break;
                    case (int)MenuItemType.PedidoPDV:
                        MenuPages.Add(id, new NavigationGradient(new VerPedidosPage()) { BarTextColor = Color.White });
                        break;
                    case (int)MenuItemType.PedidoComercial:
                        MenuPages.Add(id, new NavigationGradient(new VerPedidosPage()) { BarTextColor = Color.White });
                        break;
                    case (int)MenuItemType.AcompanharPedido:
                        MenuPages.Add(id, new NavigationGradient(new AcompanharPedidoPage()) { BarTextColor = Color.White });
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}