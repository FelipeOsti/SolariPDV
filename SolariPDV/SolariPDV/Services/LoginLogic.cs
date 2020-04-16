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
            var response = await (await WSRequest.RequestGET("TFServMInf/f_retorna_id_usuario/" + sdsUsuario)).Content.ReadAsStringAsync();
            var json = response.Replace(@"\", "").Replace("\"[", "[").Replace("]\"", "]");

            if (int.Parse(json) > 0) return true;
            return false;
        }
    }
}
