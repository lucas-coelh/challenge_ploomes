using Newtonsoft.Json;
using System.Text.RegularExpressions;
using WebAPIs.Models.External;

namespace WebAPIs.Utils.ExternalAPI
{
    public static class CnpjHelper
    {
        private static HttpClient client = new HttpClient();

        public static (string cnpj, bool valido) ValidCnpj(string cnpj)
        {
            //verifica se tem letras
            if(cnpj.Any(char.IsLetter))             
            { 
                return (cnpj, false); 
            }

            cnpj = Regex.Replace(cnpj, "[^0-9]", "");

            //verifica a quantidade de digitos
            if (cnpj.Length != 14)
            {
                return (cnpj, false);
            }

            //verifica se todos os numeros sao iguais
            if (new string(cnpj[0], 14) == cnpj)
            {
                return (cnpj, false);
            }

            return (cnpj, true);

        }

        public static async Task<PessoaJuridica> GetCnpj(string cnpj)
        {
            string url = $"https://www.receitaws.com.br/v1/cnpj/{cnpj}";
            string responseBody = await client.GetStringAsync(url);
            var result = JsonConvert.DeserializeObject<PessoaJuridica>(responseBody);

            return result;
        }
    }
}