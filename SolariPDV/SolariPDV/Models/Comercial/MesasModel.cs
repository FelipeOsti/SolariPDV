using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SolariPDV.Models.Comercial
{
    public class MesasModel
    {
        public long ID_MESA { get; set; }
        public string DS_MESA { get; set; }
        public int NR_MESA { get; set; }
        public string BO_OCUPADA { get; set; }
        public bool EstaOcupada { get { return BO_OCUPADA.ToUpper() == "T"; } }
        public string DS_OCUPADA { get { return BO_OCUPADA.ToUpper() == "T" ? "Ocupada" : "Livre"; } }
        public ImageSource ImageMesa { get { return EstaOcupada ? "mesa_ocupada.png" : "mesa_livre.png"; } }
    }
}
