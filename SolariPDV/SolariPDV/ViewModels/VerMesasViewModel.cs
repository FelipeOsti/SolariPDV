using SolariPDV.Models;
using SolariPDV.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SolariPDV.ViewModels
{
    public class VerMesasViewModel : ViewModelBase
    {
        ObservableCollection<MesasModel> lstMesas;
        public ObservableCollection<MesasModel> LstMesas { get { return lstMesas; } set { SetValue(ref lstMesas, value); } }


        public VerMesasViewModel()
        {
            GetMesas();
        }

        private async void GetMesas()
        {
            var mesaLogic = new MesaLogic();
            LstMesas = await mesaLogic.getMesas();
        }
    }
}
