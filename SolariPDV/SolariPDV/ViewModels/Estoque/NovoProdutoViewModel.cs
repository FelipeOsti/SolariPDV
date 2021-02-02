using Newtonsoft.Json;
using SolariPDV.Models;
using SolariPDV.Models.Material;
using SolariPDV.Services;
using SolariPDV.Services.Material;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SolariPDV.ViewModels.Estoque
{
    public class NovoProdutoViewModel : ViewModelBase
    {
        string _scdCodigo;
        public string scdCodigo { get { return _scdCodigo; } set { SetValue(ref _scdCodigo, value); } }

        string _sdsMaterial;
        public string sdsMaterial { get { return _sdsMaterial; } set { SetValue(ref _sdsMaterial, value); } }

        ObservableCollection<FamiliaModel> _lstFamilias;
        public ObservableCollection<FamiliaModel> lstFamilias { get { return _lstFamilias; } set { SetValue(ref _lstFamilias, value); } }

        long nidFamilia;

        internal void SetMaterial(MaterialModel material)
        {
            try
            {
                sdsMaterial = material.DS_MATERIAL;
                scdCodigo = material.CD_MATERIAL;
                nvlUnitario = material.VL_UNITAR;
                nvlCusto = material.VL_CUSTO;
                bboMateriaPrima = material.BO_MATERIAPRIMA;
                nqtInicial = material.QT_SALDO;
                BboNovo = false;
                nidFamilia = material.ID_FAMILIA;

                if(lstFamilias != null) familiaSelecionada = lstFamilias.FirstOrDefault(f => f.ID_FAMILIA == material.ID_FAMILIA);
            }
            catch { }
        }
        Boolean _bboNovo;
        public Boolean BboNovo { get { return _bboNovo; } set { SetValue(ref _bboNovo, value); } }

        public Boolean BboMostrar { get { return !_bboNovo; } }

        FamiliaModel _familiaSelecionada;
        public FamiliaModel familiaSelecionada { get { return _familiaSelecionada; } set { SetValue(ref _familiaSelecionada, value); } }

        double _nvlUnitario;
        public double nvlUnitario { get { return _nvlUnitario; } set { SetValue(ref _nvlUnitario, value); } }

        double _nvlCusto;
        public double nvlCusto { get { return _nvlCusto; } set { SetValue(ref _nvlCusto, value); } }

        double _nqtInicial;
        public double nqtInicial { get { return _nqtInicial; } set { SetValue(ref _nqtInicial, value); } }

        double _nqtSaldo;
        public double nqtSaldo { get { return _nqtSaldo; } set { SetValue(ref _nqtSaldo, value); } }

        bool _bboMateriaPrima;
        public bool bboMateriaPrima { get { return _bboMateriaPrima; } set { SetValue(ref _bboMateriaPrima, value); } }

        public NovoProdutoViewModel()
        {
            BboNovo = true;
            nidFamilia = 0;
            SalvarProdutoCommand = new Command(SalvarProduto);
            getFamilias();
            nqtSaldo = 0;
        }

        private async void getFamilias()
        {
            try
            {
                EstoqueLogic eL = new EstoqueLogic();
                var json = await eL.GetFamilias();
                lstFamilias = JsonConvert.DeserializeObject<ObservableCollection<FamiliaModel>>(json);
                if(nidFamilia > 0) familiaSelecionada = lstFamilias.FirstOrDefault(f => f.ID_FAMILIA == nidFamilia);
            }
            catch
            {

            }
        }

        private async void SalvarProduto()
        {
            EstoqueLogic eL = new EstoqueLogic();
            await eL.SalvarProduto(new Models.Material.MaterialModel()
            {
                BO_MATERIAPRIMA = bboMateriaPrima,
                CD_MATERIAL = scdCodigo,
                DS_FAMILIA = familiaSelecionada.DS_FAMILIA,
                DS_MATERIAL = sdsMaterial,
                ID_ESTABELECIMENTO = App.current.EstabSelected.ID_ESTABELECIMENTO,
                VL_UNITAR = nvlUnitario,
                VL_CUSTO = nvlCusto,
                QT_INICIAL = nqtInicial
            });
        }

        public ICommand SalvarProdutoCommand;
    }
}
