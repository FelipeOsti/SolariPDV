using SolariPDV.ViewModels;
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
    public partial class InicioPage : ContentPage
    {

        public static InicioPage current;
        public InicioViewModel inicioViewModel;
        public InicioPage()
        {
            BindingContext = inicioViewModel = new InicioViewModel();
            InitializeComponent();
            current = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(App.current.EstabSelected != null) inicioViewModel.GetDashBoardCommand.Execute(null);
        }
    }
}