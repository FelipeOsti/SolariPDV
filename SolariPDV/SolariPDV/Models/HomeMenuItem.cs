using System;
using System.Collections.Generic;
using System.Text;

namespace SolariPDV.Models
{
    public enum MenuItemType
    {
        Estoque,
        PedidoPDV,
        PedidoComercial
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
