using SolariPDV.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SolariPDV.Models
{
    public class Pedido
    {
        public static PedidoModel PedidoAtual { get; set; }

        public static async void IniciarPedido(PedidoSistemaModel ped)
        {
            PedidoAtual = new PedidoModel()
            {
                ID_PEDIDO = ped.ID_PEDIDO,
                DS_CLIENTE = ped.DS_RAZAO,
                ID_MESA = ped.ID_MESA,
                DS_MESA = ped.DS_MESA,
            };

            var pedLogic = new PedidoLogic();

            var itemPed = await pedLogic.GetItemPedido(ped.ID_PEDIDO);
            
            foreach (var item in itemPed)
            {
                PedidoAtual.Add(item);
            }
        }

        public static void IncluirItemFilho(string sflTipo, ItemPedidoModel item, int index = 0)
        {
            if (sflTipo != "I" && sflTipo != "R") return;
            var sdsPalavra = sflTipo == "I" ? "Adicionar" : "Remover";
            if (sflTipo == "R") item.VL_UNITARIO = 0;

            item.DS_MATERIAL = "     " + sdsPalavra + " " + item.DS_MATERIAL;           
            item.BO_RELACIONADO = true;
            if(index > 0)
                PedidoAtual.Insert(index,item);
            else
                PedidoAtual.Add(item);
        }
    }


    public class PedidoModel : ObservableCollection<ItemPedidoModel>
    {
        public long ID_PEDIDO { get; set; }
        public string DS_CLIENTE { get; set; }
        public long ID_MESA { get; set; }
        public string DS_MESA { get; set; }
        public string DS_TELEFONE { get; set; }
        public Double? VL_PEDIDO
        {
            get
            {
                Double? total = 0;
                foreach (var it in this)
                {
                    total = total + it.VL_TOTAL;
                }
                return total;
            }
        }
        public string DS_VLPEDIDO
        {
            get
            {
                return string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", VL_PEDIDO);
            }
        }

        internal PedidoIntegracaoModel GetPedidoIntegracao()
        {
            var ped = new PedidoIntegracaoModel() {
                ID_MESA = ID_MESA,
                ID_ESTABELECIMENTO = ID_PEDIDO == 0 ? App.current.EstabSelected.ID_ESTABELECIMENTO : 0,
                DS_CLIENTE = DS_CLIENTE,
                DS_MESA = DS_MESA,
                DS_TELEFONE = DS_TELEFONE,
                ID_PEDIDO = ID_PEDIDO,
                itens = new List<ItemPedidoModel>()
            };

            foreach (var it in this)
            {
                ped.itens.Add(it);
            }

            return ped;
        }
    }

    public class ItemPedidoModel
    {
        public long ID_ITEM { get; set; }
        public long ID_MATERIAL { get; set; }
        public string DS_MATERIAL { get; set; }
        public long? ID_TAMANHO { get; set; }
        public string DS_TAMANHO { get; set; }
        public bool isShowTamanho { get { return !string.IsNullOrEmpty(DS_TAMANHO); } }
        public double? VL_UNITARIO { get; set; }
        public double QT_PEDIDO { get; set; }
        public Boolean  BO_RELACIONADO { get; set; }
        public Boolean BO_ASSAR { get; set; }
        public double? VL_TOTAL { get { return VL_UNITARIO * QT_PEDIDO; }  }
        public string DS_VLTOTAL
        {
            get
            {
                if (VL_TOTAL == null)
                    return "R$ Váriavel";
                else
                    return string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", VL_TOTAL);
            }
        }
        public bool bboMostraQtde { get { return !BO_RELACIONADO; } }

        public ObservableCollection<FichaModel> lstFicha { get; internal set; }

        internal void AdicionarFicha(IEnumerable<FichaModel> _lst)
        {
            if (lstFicha == null) lstFicha = new ObservableCollection<FichaModel>();

            foreach (var it in _lst)
            {
                if (!lstFicha.Any(f => f.DS_MATERIAL == it.DS_MATERIAL)) lstFicha.Add(it); 
            }
        }
    }
}
