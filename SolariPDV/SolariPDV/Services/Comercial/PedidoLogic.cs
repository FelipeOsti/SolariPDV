﻿using Newtonsoft.Json;
using SolariPDV.Models;
using SolariPDV.Models.Comercial;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace SolariPDV.Services.Comercial
{
    public class PedidoLogic
    {
        public async Task<ObservableCollection<PedidoSistemaModel>> GetPedidos(string sdsFiltro, DateTime ddtIni, DateTime ddtFim, bool bboFinalizados = false, bool bboFiltro = false)
        {
            try
            {
                var sdsUrl = "";
                if (bboFiltro)
                {
                    var sdtIni = ddtIni.ToString("yyyyMMdd");
                    var sdtFim = ddtFim.AddDays(1).ToString("yyyyMMdd");
                    sdsUrl = "TFServMCOM/f_get_pedidos/" + App.current.EstabSelected.ID_ESTABELECIMENTO + "/" + sdsFiltro + "/" + sdtIni + "/" + sdtFim + "/" + bboFinalizados;
                }
                else 
                {
                    sdsUrl = "TFServMCOM/f_get_pedidos/" + App.current.EstabSelected.ID_ESTABELECIMENTO;
                }
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
                if (!string.IsNullOrEmpty(json.Replace("\"","")))
                {
                    return JsonConvert.DeserializeObject<ObservableCollection<ItemPedidoModel>>(Util.StringUnicodeToUTF8(json)); 
                }
                else
                {
                    return new ObservableCollection<ItemPedidoModel>();
                }
            }
            catch
            {
                throw;
            }
        }

        internal async Task<string> ExcluirItemPedido(long idItem)
        {
            try
            {
                var sdsUrl = "TFServMCOM/p_excluir_item_pedido/" + idItem;
                var request = await WSRequest.RequestGET(sdsUrl);

                var retorno = await request.Content.ReadAsStringAsync();
                return retorno.Replace(@"\", "").Replace("\"[", "[").Replace("]\"", "]");                
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
                jsonRetorno = jsonRetorno.Replace(@"\", "").Replace("\"[", "[").Replace("]\"", "]");
                jsonRetorno = jsonRetorno.Replace(@"\", "").Replace("\"[", "[").Replace("]\"", "]").Trim('\"');
                var pedidoIntegracao = JsonConvert.DeserializeObject<PedidoIntegracaoModel>(Util.StringUnicodeToUTF8(jsonRetorno));

                if (pedidoIntegracao.ID_PEDIDO == 0) throw new Exception();

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
            catch (Exception ex)
            {
                throw;
            }
        }

        internal async Task<string> FinalizarCozinha(long nidPedido)
        {
            try
            {
                var sdsUrl = "TFServMCOM/p_finalizar_cozinha/"+ nidPedido;
                var response = await WSRequest.RequestGET(sdsUrl);
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                throw;
            }
        }

        internal async Task<string> IniciarCozinha(long nidPedido)
        {
            try
            {
                var sdsUrl = "TFServMCOM/p_iniciar_cozinha/" + nidPedido;
                var response = await WSRequest.RequestGET(sdsUrl);
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                throw;
            }
        }

        internal async Task FinalizarPedido(PedidoModel pedidoAtual)
        {
            try
            {
                var json = JsonConvert.SerializeObject(pedidoAtual.GetPedidoIntegracao());

                var sdsUrl = "TFServMCOM/p_gera_pedido/";
                var request = await WSRequest.RequestPOST(sdsUrl, json);
            }
            catch
            {
                throw;
            }
        }
    }
}