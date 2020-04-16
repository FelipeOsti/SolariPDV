using Newtonsoft.Json;
using SolariPDV.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SolariPDV.Services
{
    public class MenuLogic
    {
        public async Task<DadosAcessoModel> GetDadosMenu()
        {
            try
            {
                var retorno = await(await WSRequest.RequestGET("TFServMINF/f_get_menu_dados/" + App.current.sdsUsuario)).Content.ReadAsStringAsync();

                retorno = retorno.Replace(@"\", "").Replace("\"[", "[").Replace("]\"", "]").Replace("\"{", "{").Replace("}\"", "}");

                return JsonConvert.DeserializeObject<DadosAcessoModel>(Util.StringUnicodeToUTF8(retorno));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
