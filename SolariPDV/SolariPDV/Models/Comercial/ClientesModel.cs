using System;
using System.Collections.Generic;
using System.Text;

namespace SolariPDV.Models.Comercial
{
    public class ClientesModel
    {
        public long ID_CLIENTE { get; set; }
        public string DS_RAZAO { get; set; }
        public string DS_FANTASIA { get; set; }
        public string NR_CELULAR { get; set; }
        public string NR_TELEFONE { get; set; }
        public string DS_ENDERECO { get; set; }
        public string DS_CIDADE { get; set; }
        public string NR_CEP { get; set; }
    }
}
