using System;
using System.Collections.Generic;
using System.Text;

namespace SolariPDV.Services.Comercial
{
    public class ClientesLogic
    {
        public async System.Threading.Tasks.Task<string> GetClientes(string sdsFiltro)
        {
            try
            {
                var retorno = await (await WSRequest.RequestGET("TFServMCOM/f_get_clientes_app/"+sdsFiltro)).Content.ReadAsStringAsync();

                retorno = retorno.Replace(@"\", "").Replace("\"[", "[").Replace("]\"", "]");

                return Util.StringUnicodeToUTF8(retorno);
            }
            catch
            {
                throw;
            }
        }
    }
}
