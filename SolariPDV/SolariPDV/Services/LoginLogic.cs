using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SolariPDV.Services
{
    class LoginLogic
    {
        public async Task<bool> VerificaLogin(string sdsUsuario)
        {
            await GetHostName(sdsUsuario);

            var response = await (await WSRequest.RequestGET("TFServMInf/f_retorna_id_usuario/" + sdsUsuario)).Content.ReadAsStringAsync();
            var json = response.Replace(@"\", "").Replace("\"[", "[").Replace("]\"", "]");

            if (int.Parse(json) > 0)
            {
                return true;
            }
            return false;
        }

        public async Task GetHostName(string sdsUsuario)
        {
            App.current.sdsHostName = await BuscarHostName(sdsUsuario);
            if (string.IsNullOrEmpty(App.current.sdsHostName)) App.current.sdsHostName = App.sdsServidorApp;
        }

        public async Task<string> BuscarHostName(string sdsUsuario)
        {
            try
            {
                var response = await(await WSRequest.RequestGETSolari("TFDM_CONEXAO/f_host_name_login/" + sdsUsuario)).Content.ReadAsStringAsync();
                var json = response.Replace("\"", "").Replace("\"[", "[").Replace("]\"", "]");
                
                return json;
            }
            catch
            {
                return null;
            }
        }
    }
}
