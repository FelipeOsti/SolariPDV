using SolariPDV.Models;
using SolariPDV.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace SolariPDV.Services
{
    public class MaterialLogic
    {
        internal async Task<string> GetMateriais(string filtro)
        {
            try
            {
                var retorno = await(await WSRequest.RequestGET("TFServMMAT/f_get_materiais/" + App.current.EstabSelected.ID_ESTABELECIMENTO+ "/"+ filtro)).Content.ReadAsStringAsync();

                retorno = retorno.Replace(@"\", "").Replace("\"[", "[").Replace("]\"", "]");

                return Util.StringUnicodeToUTF8(retorno);
            }
            catch
            {
                return null;
            }
        }
    }
}
