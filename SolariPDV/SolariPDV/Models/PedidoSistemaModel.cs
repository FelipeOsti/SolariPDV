using SolariPDV.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SolariPDV.Models
{
    public class PedidoSistemaModel : ViewModelBase
    {
        public long ID_PEDIDO { get; set; }

        string _sdsOrdem;
        public string sdsOrdem { get { return _sdsOrdem; } set { _sdsOrdem = value; OnPropertyChanged(); } }
        public string DS_RAZAO { get; set; }
        public string DS_SITDOCTO { get; set; }
        public double VL_TOTAL { get; set; }
        public long ID_MESA { get; set; }
        public string DS_MESA { get; set; }
        public string DT_EMISSAO { get; set; }

        string sdtInicioProducao;
        public string DT_INICIOPRODUCAO { get { return sdtInicioProducao; } set { sdtInicioProducao = value; OnPropertyChanged(); DescricaoBotao(); } }

        private void DescricaoBotao()
        {
            sdsBtIniciar = String.IsNullOrEmpty(sdtInicioProducao) ? "Iniciar" : String.IsNullOrEmpty(sdtFimProducao) ? "Finalizar" : "Finalizado";
            bboBtIniciarAtivado = sdsBtIniciar != "Finalizado"; 
        }

        string sdtFimProducao;
        public string DT_FIMPRODUCAO { get { return sdtFimProducao; } set { sdtFimProducao = value; OnPropertyChanged(); DescricaoBotao(); } }        
        public string DT_PREVENTREGA { get; set; }
        public ObservableCollection<ItemPedidoModel> itens { get; set; }

        string _sdsBtINiciar;
        public string sdsBtIniciar { get { return _sdsBtINiciar; } set { _sdsBtINiciar = value; OnPropertyChanged(); } }
        Boolean _bboBtIniciarAtivado;
        public Boolean bboBtIniciarAtivado { get { return _bboBtIniciarAtivado; } set { _bboBtIniciarAtivado = value; OnPropertyChanged(); } }

        private string sdsItens;
        public string DS_ITENS { get { return sdsItens; } set { sdsItens = value; OnPropertyChanged(); } }
        public string DS_VLTOTAL
        {
            get
            {
                return string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", VL_TOTAL);
            }
        }
        public double widthRequest 
        {
            get 
            {
                if (DeviceInfo.Idiom == DeviceIdiom.Tablet) return Application.Current.MainPage.Width - 250;
                return Application.Current.MainPage.Width - 55; 
            } 
        }
    }
}

