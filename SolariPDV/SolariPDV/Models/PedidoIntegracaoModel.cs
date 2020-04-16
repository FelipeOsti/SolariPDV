using System;
using System.Collections.Generic;
using System.Text;

namespace SolariPDV.Models
{
    public class PedidoIntegracaoModel
    {
        public long ID_PEDIDO { get; set; }
        public long ID_ESTABELECIMENTO { get; set; }
        public string DS_CLIENTE { get; set; }
        public long ID_MESA { get; set; }
        public string DS_MESA { get; set; }
        public string DS_TELEFONE { get; set; }
        public List<ItemPedidoModel> itens { get; set; }
    }
}
