using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SolariPDV.ViewModels.PopUps;
using SolariPDV.Views.Estoque;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SolariPDV.Views.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NovaMovimPopUp : PopupPage
    {
        long nidMaterial;
        NovaMovimViewModel novaMovimViewModel;
        public NovaMovimPopUp(long nid, string sdsMaterial)
        {
            nidMaterial = nid;
            BindingContext = novaMovimViewModel = new NovaMovimViewModel();
            InitializeComponent();
            novaMovimViewModel.DS_MATERIAL = sdsMaterial;
        }

        private void btCancelar_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void btConfirmar_Clicked(object sender, EventArgs e)
        {
            novaMovimViewModel.GravarMovim(nidMaterial);
            PopupNavigation.Instance.PopAsync();
        }
    }
}