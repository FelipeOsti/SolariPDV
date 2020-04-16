using System;
using System.Collections.Generic;
using System.Text;

namespace SolariPDV.Models
{
    public class EstoqueModel
    {
        public string CD_MATERIAL { get; set; }
        public string DS_MATERIAL { get; set; }
        public long ID_SALDOMAT { get; set; }
        public long ID_MATERIAL { get; set; }
        public float QT_SALDO { get; set; }
        public float VL_SALDO { get; set; }
        public float VL_UNITAR { get; set; }
        public string svlUnitar { get{
                return VL_UNITAR.ToString("C");
            }
        }
        public long ID_ESTABELECIMENTO   { get; set; }
        public string DS_ESTABELECIMENTO { get; set; }
    }
}
