using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SolariPDV.Services
{
    class WSRequest
    {
        static string sdsPrefix = "datasnap/rest/";

        public static async Task<HttpResponseMessage> RequestGET(string sdsUrl)
        {
            try
            {

                var client = new HttpClient() { BaseAddress = new Uri("http://" + App.current.sdsServidorApp + ":" + App.current.nnrPorta) };

                var byteArray = Encoding.ASCII.GetBytes(App.current.sdsUsuario + ":" + App.current.sdsSenha);
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");

                var response = await client.GetAsync(sdsPrefix + sdsUrl);

                response.EnsureSuccessStatusCode();

                return response;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        internal static async Task<HttpResponseMessage> RequestPOST(string sdsUrl, string json)
        {
            try
            {

                var client = new HttpClient() { BaseAddress = new Uri("http://" + App.current.sdsServidorApp + ":" + App.current.nnrPorta) };

                var byteArray = Encoding.ASCII.GetBytes(App.current.sdsUsuario + ":" + App.current.sdsSenha);
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");

                var response = await client.PostAsync(sdsPrefix + sdsUrl, new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
