using System;
using System.Collections.Generic;
using System.Text;

namespace SolariPDV.Models.Material
{
    public class MaterialModel
    {
        public long ID_MATERIAL { get; set; }
        public string CD_MATERIAL { get; set; }
        public string DS_MATERIAL { get; set; }
        public bool BO_MATERIAPRIMA { get; set; }
        public double VL_UNITAR { get; set; }
        public double VL_CUSTO { get; set; }
        public string DS_FAMILIA { get; set; }
        public long ID_ESTABELECIMENTO { get; set; }
        public double QT_INICIAL { get; set; }
        public double QT_SALDO { get; set; }
        public int ID_FAMILIA { get; set; }
    }

    public class FamiliaModel
    {
        public int ID_FAMILIA { get; set; }
        public string DS_FAMILIA { get; set; }
    }
}
