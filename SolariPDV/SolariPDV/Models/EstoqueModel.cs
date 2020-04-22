using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SolariPDV.Models
{
    public class EstoqueModel
    {
        CultureInfo MinhaCultura = CultureInfo.GetCultureInfo("pt-BR");

        public string CD_MATERIAL { get; set; }
        public string DS_MATERIAL { get; set; }
        public long ID_SALDOMAT { get; set; }
        public long ID_MATERIAL { get; set; }
        public float QT_SALDO { get; set; }
        public float VL_SALDO { get; set; }
        public float VL_UNITAR { get; set; }
        public string svlUnitar { get{
                return VL_UNITAR.ToString("C", MinhaCultura);
            }
        }
        public long ID_ESTABELECIMENTO   { get; set; }
        public string DS_ESTABELECIMENTO { get; set; }

        public Color corSaldo { get { return QT_SALDO < 0 ? Color.DarkRed : Color.DarkBlue; } }
    }

    public class MovimEstoqueModel
    {
        public long ID_MATERIAL { get; set; }
        public string DS_MATERIAL { get; set; }
        public double QT_MOVIM { get; set; }
        public double VL_MOVIM { get; set; }
        public string DT_MOVIM { get; set; }
        public string DS_TPMOVIM { get; set; }
        public string DS_HISTORICO { get; set; }
    }

    public class TipoMovimModel
    {
        public long ID_TPMOVIM { get; set; }
        public string DS_TPMOVIM { get; set; }
    }
}
