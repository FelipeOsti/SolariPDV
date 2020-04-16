using System;
using System.Collections.Generic;
using System.Text;

namespace SolariPDV.Models
{
    public class DadosAcessoModel
    {
        public long ID_USUARIO { get; set; }
        public string DS_USUARIO { get; set; }
        public List<EstabelecimentoModel> lstEstabelecimento { get; set; }
    }
}
