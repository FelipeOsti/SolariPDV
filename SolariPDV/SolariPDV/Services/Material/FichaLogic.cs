using SolariPDV.Models;
using SolariPDV.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SolariPDV.Logic.Material
{
    class FichaLogic
    {
        public async Task<string> GetFicha(long nidMaterial)
        {
            try
            {
                var retorno = await(await WSRequest.RequestGET("TFServMMAT/f_get_ficha/" + nidMaterial)).Content.ReadAsStringAsync();

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
