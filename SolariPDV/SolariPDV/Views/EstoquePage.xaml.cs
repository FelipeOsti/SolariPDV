
using FormsCurvedBottomNavigation;
using Newtonsoft.Json;
using SolariPDV.Models;
using SolariPDV.Services;
using SolariPDV.ViewModels;
using SolariPDV.Views.Estoque;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SolariPDV.Views
{
    [DesignTimeVisible(false)]
    public partial class EstoquePage : CurvedBottomTabbedPage //ContentPage
    {
        public EstoquePage()
        {
            InitializeComponent();
            IniciarPaginas();
        }

        private void IniciarPaginas()
        {
            this.Children.Add(new EstoqueConsultaPage(false)
            {
                Title = "Estoque",
                IconImageSource = "estoque"
            });
            this.Children.Add(new MovimentacaoEstoquePage()
            {
                Title = "Movimentar",
                IconImageSource = "movimentacao"
            });
            this.Children.Add(new FamiliaEstoquePage()
            {
                Title = "Família",
                IconImageSource = "familia"
            });
            this.Children.Add(new EstoqueConsultaPage(true)
            {
                Title = "Ler Barra",
                IconImageSource = "camera"
            });
        }

        private void CurvedBottomTabbedPage_FabIconClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NovoProdutoPage());
        }

        public void btScanClicked(object sender, EventArgs e)
        {
            //estoqueViewModel.EscanearCodigoBarrasCommand.Execute(null);
        }
        
    }
}