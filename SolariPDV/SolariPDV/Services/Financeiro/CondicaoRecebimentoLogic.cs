using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace SolariPDV.Services.Financeiro
{
    public class CondicaoRecebimentoLogic
    {
        public async Task<string> GetCondicoes()
        {
            try
            {
                var retorno = await(await WSRequest.RequestGET("TFServMFIN/f_consulta_condreceb")).Content.ReadAsStringAsync();

                retorno = retorno.Replace(@"\", "").Replace("\"[", "[").Replace("]\"", "]");

                return Util.StringUnicodeToUTF8(retorno);
            }
            catch { throw; }
        }
    }
}
