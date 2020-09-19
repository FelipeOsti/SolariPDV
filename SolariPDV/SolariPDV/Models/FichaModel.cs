using System;
using System.Collections.Generic;
using System.Text;

namespace SolariPDV.Models
{
    public class FichaModel
    {
        public long ID_FICHA { get; set; }
        public long ID_MATERIAL { get; set; }
        public long ID_TAMANHO { get; set; }
        public long ID_MATERIAL_1 { get; set; }
        public string DS_MATERIAL { get; set; }
        public bool BO_SELECIONA { get; set; }
        public double QT_QUANTIDADE { get; set; }
    }
}
