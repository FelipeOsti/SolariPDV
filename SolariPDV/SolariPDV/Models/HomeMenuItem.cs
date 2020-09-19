using System;
using System.Collections.Generic;
using System.Text;

namespace SolariPDV.Models
{
    public enum MenuItemType
    {
        Inicio,
        Estoque,
        PedidoPDV,
        PedidoComercial,
        AcompanharPedido
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
