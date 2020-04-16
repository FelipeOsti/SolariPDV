﻿using Newtonsoft.Json;
using SolariPDV.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace SolariPDV.Services
{
    public class PedidoLogic
    {
        public async Task<ObservableCollection<PedidoSistemaModel>> GetPedidosPendentes()
        {
            try
            {
                var sdsUrl = "TFServMCOM/f_get_pedidos_aguardando/"+App.current.EstabSelected.ID_ESTABELECIMENTO;
                var request = await WSRequest.RequestGET(sdsUrl);

                var json = await request.Content.ReadAsStringAsync();
                json = json.Replace(@"\", "").Replace("\"[", "[").Replace("]\"", "]");
                return JsonConvert.DeserializeObject<ObservableCollection<PedidoSistemaModel>>(Util.StringUnicodeToUTF8(json));
            }
            catch
            {
                throw;
            }
        }

        internal async Task<ObservableCollection<ItemPedidoModel>> GetItemPedido(long iD_PEDIDO)
        {
            try
            {
                var sdsUrl = "TFServMCOM/f_get_itens_pedido/"+iD_PEDIDO;
                var request = await WSRequest.RequestGET(sdsUrl);

                var json = await request.Content.ReadAsStringAsync();
                json = json.Replace(@"\", "").Replace("\"[", "[").Replace("]\"", "]");
                return JsonConvert.DeserializeObject<ObservableCollection<ItemPedidoModel>>(Util.StringUnicodeToUTF8(json));
            }
            catch
            {
                throw;
            }
        }

        public async Task<PedidoModel> SalvarPedido(PedidoModel pedido)
        {
            try
            {
                var json = JsonConvert.SerializeObject(pedido.GetPedidoIntegracao());

                var sdsUrl = "TFServMCOM/p_gera_pedido/";
                var request = await WSRequest.RequestPOST(sdsUrl,json);

                var jsonRetorno = await request.Content.ReadAsStringAsync();
                jsonRetorno = json.Replace(@"\", "").Replace("\"[", "[").Replace("]\"", "]");
                var pedidoIntegracao = JsonConvert.DeserializeObject<PedidoIntegracaoModel>(Util.StringUnicodeToUTF8(json));

                var pedidoRetorno = new PedidoModel()
                {
                    ID_PEDIDO = pedidoIntegracao.ID_PEDIDO,
                    ID_MESA = pedidoIntegracao.ID_MESA,
                    DS_CLIENTE = pedidoIntegracao.DS_CLIENTE,
                    DS_MESA = pedidoIntegracao.DS_MESA,
                    DS_TELEFONE = pedidoIntegracao.DS_TELEFONE
                };
                foreach (var it in pedidoIntegracao.itens) pedidoRetorno.Add(it);

                return pedidoRetorno;
            }
            catch
            {
                throw;
            }
        }
    }
}