using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SolariPDV.Models
{
    public class PedidoSistemaModel
    {
        public long ID_PEDIDO { get; set; }
        public string DS_RAZAO { get; set; }
        public string DS_SITDOCTO { get; set; }
        public double VL_TOTAL { get; set; }
        public long ID_MESA { get; set; }
        public string DS_MESA { get; set; }
        public string DT_EMISSAO { get; set; }

        public string DS_VLTOTAL
        {
            get
            {
                return string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", VL_TOTAL);
            }
        }
    }
}

