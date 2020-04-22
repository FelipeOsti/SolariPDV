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
    public class NovoProdutoViewModel : ViewModelBase
    {
        string _scdCodigo;
        public string scdCodigo { get { return _scdCodigo; } set { SetValue(ref _scdCodigo, value); } }

        string _sdsMaterial;
        public string sdsMaterial { get { return _sdsMaterial; } set { SetValue(ref _sdsMaterial, value); } }

        ObservableCollection<FamiliaModel> _lstFamilias;
        public ObservableCollection<FamiliaModel> lstFamilias { get { return _lstFamilias; } set { SetValue(ref _lstFamilias, value); } }

        FamiliaModel _familiaSelecionada;
        public FamiliaModel familiaSelecionada { get { return _familiaSelecionada; } set { SetValue(ref _familiaSelecionada, value); } }

        double _nvlUnitario;
        public double nvlUnitario { get { return _nvlUnitario; } set { SetValue(ref _nvlUnitario, value); } }

        double _nvlCusto;
        public double nvlCusto { get { return _nvlCusto; } set { SetValue(ref _nvlCusto, value); } }

        double _nqtInicial;
        public double nqtInicial { get { return _nqtInicial; } set { SetValue(ref _nqtInicial, value); } }

        bool _bboMateriaPrima;
        public bool bboMateriaPrima { get { return _bboMateriaPrima; } set { SetValue(ref _bboMateriaPrima, value); } }

        public NovoProdutoViewModel()
        {
            SalvarProdutoCommand = new Command(SalvarProduto);
            getFamilias(); 
        }

        private async void getFamilias()
        {
            try
            {
                EstoqueLogic eL = new EstoqueLogic();
                var json = await eL.GetFamilias();
                lstFamilias = JsonConvert.DeserializeObject<ObservableCollection<FamiliaModel>>(json);
            }
            catch
            {

            }
        }

        private async void SalvarProduto()
        {
            EstoqueLogic eL = new EstoqueLogic();
            await eL.SalvarProduto(new Models.MaterialModel()
            {
                BO_MATERIAPRIMA = bboMateriaPrima,
                CD_MATERIAL = scdCodigo,
                DS_FAMILIA = familiaSelecionada.DS_FAMILIA,
                DS_MATERIAL = sdsMaterial,
                ID_ESTABELECIMENTO = App.current.EstabSelected.ID_ESTABELECIMENTO,
                VL_UNITARIO = nvlUnitario,
                VL_CUSTO = nvlCusto,
                QT_INICIAL = nqtInicial
            });
        }

        public ICommand SalvarProdutoCommand;
    }
}
