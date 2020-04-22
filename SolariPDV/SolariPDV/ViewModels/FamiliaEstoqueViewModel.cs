using Newtonsoft.Json;
using SolariPDV.Models;
using SolariPDV.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SolariPDV.ViewModels
{
    public class FamiliaEstoqueViewModel : ViewModelBase
    {
        ObservableCollection<FamiliaModel> _lstFamilias;
        public ObservableCollection<FamiliaModel> lstFamilias { get { return _lstFamilias; } set { SetValue(ref _lstFamilias, value); } }

        ICommand _GetFamiliasCommand;
        public ICommand GetFamiliasCommand { get { return _GetFamiliasCommand; } set { SetValue(ref _GetFamiliasCommand, value); } }

        public FamiliaEstoqueViewModel()
        {
            GetFamiliasCommand = new Command(BuscarFamilias);
        }

        public async void EditarFamilia(long nid,string sdsFamilia)
        {
            try
            {
                EstoqueLogic el = new EstoqueLogic();
                var familia = await Application.Current.MainPage.DisplayPromptAsync("Editar Familia", "Altere o nome da Família",placeholder:sdsFamilia);
                if (!String.IsNullOrEmpty(familia))
                    await el.SalvarFamilia(nid, familia);

                BuscarFamilias();
            }
            catch { }
        }

        private async void BuscarFamilias()
        {
            try
            {
                IsBusy = true;
                try
                {
                    EstoqueLogic el = new EstoqueLogic();
                    var json = await el.GetFamilias();
                    lstFamilias = JsonConvert.DeserializeObject<ObservableCollection<FamiliaModel>>(json);
                }
                catch { }
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void AdicionarFamilia()
        {
            try
            {
                EstoqueLogic el = new EstoqueLogic();
                var familia = await Application.Current.MainPage.DisplayPromptAsync("Nova Familia", "Qual o nome da nova Familia?");
                if (!String.IsNullOrEmpty(familia))
                    await el.SalvarFamilia(0, familia);

                BuscarFamilias();
            }
            catch { }
        }
    }
}
