using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SolariPDV.Services
{
    public class DashboardLogic
    {
        public async Task<string> GetPedidosMes()
        {
            try
            {
                var retorno = await(await WSRequest.RequestGET("TFServMCOM/f_get_faturado_mes/" + App.current.EstabSelected.ID_ESTABELECIMENTO)).Content.ReadAsStringAsync();

                retorno = retorno.Replace(@"\", "").Replace("\"[", "[").Replace("]\"", "]");

                return retorno;
            }
            catch
            {
                return null;
            }
        }

    }
}
