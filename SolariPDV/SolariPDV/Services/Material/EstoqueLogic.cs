using Newtonsoft.Json;
using SolariPDV.Models;
using SolariPDV.Models.Material;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SolariPDV.Services.Material
{
    public class EstoqueLogic
    {
        public async Task<string> GetEstoque(string sdsFiltro)
        {
            try
            {
                var retorno = await (await WSRequest.RequestGET("TFServMMAT/f_get_saldo_estoque/" + sdsFiltro)).Content.ReadAsStringAsync();

                retorno = retorno.Replace(@"\", "").Replace("\"[", "[").Replace("]\"", "]");

                return Util.StringUnicodeToUTF8(retorno);
            }
            catch
            {
                return null;
            }
        }

        public async Task<string> SalvarProduto(MaterialModel mater)
        {
            try 
            { 
                mater.ID_ESTABELECIMENTO = App.current.EstabSelected.ID_ESTABELECIMENTO;
                var json = JsonConvert.SerializeObject(mater);
                var retorno = await (await WSRequest.RequestPOST("TFServMMAT/f_novo_produto/",json)).Content.ReadAsStringAsync();
                retorno = retorno.Replace(@"\", "").Replace("\"[", "[").Replace("]\"", "]");
                return Util.StringUnicodeToUTF8(retorno);
            }
            catch
            {
                return null;
            }
        }

        public async Task SalvarFamilia(long nid, string descri)
        {
            try {
                await (await WSRequest.RequestGET("TFServMMAT/f_salvar_familia/" + App.current.EstabSelected.ID_ESTABELECIMENTO + "/" + nid + "/" + descri)).Content.ReadAsStringAsync();
            }
            catch
            {

            }
        }

        public async Task SalvarMovimEstoque(long nidMater, long nidTpMovim, double nvlMovim, double nqtMovim, string sdsHistorico )
        {
            try
            {
                await (await WSRequest.RequestGET("TFServMMAT/f_movim_estoque_manual_app/" + App.current.EstabSelected.ID_ESTABELECIMENTO + "/" + nidMater + "/" + nidTpMovim + "/" + nvlMovim.ToString().Replace(".","").Replace(",",".") + "/" + nqtMovim.ToString().Replace(".", "").Replace(",", ".") + "/" + sdsHistorico)).Content.ReadAsStringAsync();
            }
            catch
            {

            }
        }

        public async Task<string> GetFamilias()
        {
            try
            {
                var retorno = await (await WSRequest.RequestGET("TFServMMAT/f_get_familias/")).Content.ReadAsStringAsync();
                retorno = retorno.Replace(@"\", "").Replace("\"[", "[").Replace("]\"", "]");
                return Util.StringUnicodeToUTF8(retorno);
            }
            catch
            {
                return null;
            }
        }

        public async Task<string> GetMovimeEstoque(string sdsFiltro)
        {
            try
            {
                var retorno = await (await WSRequest.RequestGET("TFServMMAT/f_get_movimentacao/" + App.current.EstabSelected.ID_ESTABELECIMENTO + "/" + sdsFiltro)).Content.ReadAsStringAsync();
                retorno = retorno.Replace(@"\", "").Replace("\"[", "[").Replace("]\"", "]");
                return Util.StringUnicodeToUTF8(retorno);
            }
            catch
            {
                return null;
            }
        }

        public async Task<string> GetTipoMovim()
        {
            try
            {
                var retorno = await (await WSRequest.RequestGET("TFServMMAT/f_get_tipo_movim/")).Content.ReadAsStringAsync();
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
