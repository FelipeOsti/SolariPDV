using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;

namespace SolariPDV.Models.Material
{
    public class CardapioModel
    {
        public long ID_CATEGORIA { get; set; }
        public string DS_CATEGORIA { get; set; }
        public long ID_MATERIAL { get; set; }
        public string DS_MATERIAL { get; set; }
        public long ID_TAMANHO { get; set; }
        public string DS_TAMANHO { get; set; }
        public double? VL_UNITARIO { get; set; }
        public string FL_ADICIONAL { get; set; }
        public string FL_PERMITEADICIONAL { get; set; }
        public bool FL_ASSAR { get; set; }
        public string DS_FICHA { get; set; }
    }

    public class CardapioCateg : List<CardapioProd>
    {
        public long ID_CATEGORIA { get; set; }
        public string DS_CATEGORIA { get; set; }
        public bool FL_ADICIONAL { get; set; }
        public bool FL_PERMITEADICIONAL { get; set; }
        public bool FL_ASSAR { get; set; }
    }

    public class CardapioProd : List<TamanhoProd>
    {
        public long ID_MATERIAL { get; set; }
        public string DS_MATERIAL { get; set; }
        public double? VL_UNITARIO { get; set; }
        public string DS_VLUNITARIO { get {
                if (VL_UNITARIO == null)
                    return "R$ Váriavel";
                else
                    return string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", VL_UNITARIO);
            }
        }
        public string DS_FICHA { get; set; }
        public IEnumerable<FichaModel> lstFicha { get; internal set; }
        public bool BO_SELECIONA { get; set; }
    }

    public class TamanhoProd
    {
        public long ID_TAMANHO { get; set; }
        public string DS_TAMANHO { get; set; }
        public double? VL_UNITARIO { get; set; }
        public string DS_VLUNITARIO { get { return string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", VL_UNITARIO); } }
    }
}
