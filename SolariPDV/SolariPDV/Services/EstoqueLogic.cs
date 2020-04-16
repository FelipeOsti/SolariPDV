using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SolariPDV.Services
{
    public class EstoqueLogic
    {
        public async Task<string> GetEstoque(string scdCodigo)
        {
            var retorno = await (await WSRequest.RequestGET("TFServMMAT/f_get_saldo_estoque/" + scdCodigo)).Content.ReadAsStringAsync();

            retorno = retorno.Replace(@"\", "").Replace("\"[", "[").Replace("]\"", "]");

            return Util.StringUnicodeToUTF8(retorno);
        }
    }
}
