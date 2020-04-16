
using Newtonsoft.Json;
using SolariPDV.Models;
using SolariPDV.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SolariPDV.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstoquePage : ContentPage
    {

        public ObservableCollection<EstoqueModel> lstEstoque = new ObservableCollection<EstoqueModel>();

        public EstoquePage()
        {
            InitializeComponent();
        }

        public async void btScanClicked(object sender, EventArgs e)
        {
            var scanner = DependencyService.Get<IQrCodeScanningService>();
            var result = await scanner.ScanAsync();
            if (!string.IsNullOrEmpty(result))
            {
                lstEstoque = await BuscarEstoque(result);
                listViewEstoque.ItemsSource = lstEstoque;
                lblMsg.IsVisible = false;
            }
            else
            {
                lblMsg.IsVisible = true;
            }
        }

        private async void btMovimentarEstoque(object sender, EventArgs e)
        {
            await DisplayAlert("Novo", "Em breve", "OK");
        }

        private async Task<ObservableCollection<EstoqueModel>> BuscarEstoque(string scdCodigo)
        {
            EstoqueLogic el = new EstoqueLogic();
            string estoque = await el.GetEstoque(scdCodigo);

            ObservableCollection<EstoqueModel> retorno = JsonConvert.DeserializeObject<ObservableCollection<EstoqueModel>>(estoque);

            return retorno;
        }
    }
}