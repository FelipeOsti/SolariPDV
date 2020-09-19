
using FormsCurvedBottomNavigation;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using SolariPDV.Models;
using SolariPDV.Services;
using SolariPDV.ViewModels;
using SolariPDV.Views.Estoque;
using SolariPDV.Views.PopUps;
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
            this.Children.Add(new ConsultaProdutosPage()
            {
                Title = "Produtos",
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
            //Navigation.PushAsync(new MovimentacaoEstoquePage());
            MaterialModel material = null;
            if (this.SelectedItem != null)
            {
                if (this.SelectedItem.GetType() == typeof(EstoqueConsultaPage)) material = EstoqueConsultaPage.EstoqueConsultaPageCurrent.GetMaterialSelecionado();
                if (this.SelectedItem.GetType() == typeof(ConsultaProdutosPage)) material = ConsultaProdutosPage.ConsultaProdutoPageCurrent.GetMaterialSelecionado();
            }
            else
                material = EstoqueConsultaPage.EstoqueConsultaPageCurrent.GetMaterialSelecionado();

            if (material == null)
            {
                DisplayAlert("Selecione", "Selecione um produto antes de movimentar", "Ok");
                this.SelectedItem = this.Children[0];
                return;
            }
            PopupNavigation.Instance.PushAsync(new NovaMovimPopUp(material.ID_MATERIAL, material.DS_MATERIAL));
        }

        public void btScanClicked(object sender, EventArgs e)
        {
            //estoqueViewModel.EscanearCodigoBarrasCommand.Execute(null);
        }
        
    }
}