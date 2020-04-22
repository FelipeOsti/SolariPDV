using Newtonsoft.Json;
using SolariPDV.Models;
using SolariPDV.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SolariPDV.ViewModels.PopUps
{
    public class NovaMovimViewModel : ViewModelBase
    {
        string sdsMaterial;
        public string DS_MATERIAL { get { return sdsMaterial; } set { SetValue(ref sdsMaterial, value); } }
        string sdsHistorico;
        public string DS_HISTORICO { get { return sdsHistorico; } set { SetValue(ref sdsHistorico, value); } }
        double nvlMovim;
        public double VL_MOVIM { get { return nvlMovim; } set { SetValue(ref nvlMovim, value); } }
        double nqtMovim;
        public double QT_MOVIM { get { return nqtMovim; } set { SetValue(ref nqtMovim, value); } }

        TipoMovimModel _TpMovimSelected;
        public TipoMovimModel TpMovimSelected { get { return _TpMovimSelected; } set { SetValue(ref _TpMovimSelected, value); } }

        ObservableCollection<TipoMovimModel> _lstTpMovim;

        internal async void GravarMovim(long nid)
        {
            try
            {
                EstoqueLogic el = new EstoqueLogic();
                await el.SalvarMovimEstoque(nid, TpMovimSelected.ID_TPMOVIM, VL_MOVIM, QT_MOVIM, DS_HISTORICO);
            }
            catch
            {

            }
        }

        public ObservableCollection<TipoMovimModel> lstTpMovim { get { return _lstTpMovim; } set { SetValue(ref _lstTpMovim, value); } }

        public NovaMovimViewModel()
        {
            GetTipoMovim();           
        }

        private async void GetTipoMovim()
        {
            try
            {
                EstoqueLogic el = new EstoqueLogic();
                var json = await el.GetTipoMovim();
                lstTpMovim = JsonConvert.DeserializeObject<ObservableCollection<TipoMovimModel>>(json);
            }
            catch
            {

            }
        }
    }
}
