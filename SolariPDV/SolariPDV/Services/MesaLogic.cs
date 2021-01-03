using Newtonsoft.Json;
using SolariPDV.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace SolariPDV.Services
{
    class MesaLogic
    {
        public async Task<ObservableCollection<MesasModel>> getMesas()
        {
            try
            {
                var retorno = await(await WSRequest.RequestGET("TFServMCOM/f_get_mesas")).Content.ReadAsStringAsync();

                retorno = retorno.Replace(@"\", "").Replace("\"[", "[").Replace("]\"", "]");

                return JsonConvert.DeserializeObject<ObservableCollection<MesasModel>>(retorno);
            }
            catch
            {
                return null;
            }
            
        }

        public async Task<ObservableCollection<PedidoSistemaModel>> GetPedido(long nidMesa)
        {
            try
            {
                var sdsUrl = "";

                sdsUrl = "TFServMCOM/f_get_pedido_mesa/" + App.current.EstabSelected.ID_ESTABELECIMENTO + "/" + nidMesa;
                
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
    }
}
