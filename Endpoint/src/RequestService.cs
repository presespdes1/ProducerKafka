using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Endpoint.src
{
    public class RequestService : IRequestService
    {
        private readonly IConfiguration _config;

        public RequestService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<string> GetRequest(string option)
        {
            string uriBase = _config.GetSection("EndPoint")["Base"];
            string token = _config.GetSection("EndPoint")["Token"];
            string api = _config.GetSection("EndPoint")["Api"];
            string noApi = _config.GetSection("EndPoint")["Option"];

            if (!int.TryParse(option, out int result) || (result < 1 || result > Convert.ToInt32(noApi)))
                return $"El valor {option} no es número entero entre 1 y {noApi}";
          
            string end = _config.GetSection("EndPoint")[$"End{option}"];

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(uriBase);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            try
            {
                HttpResponseMessage response = await client.GetAsync($"{api}{end}{token}");

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    return $"Status: {response.StatusCode}  Reason: {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                return $"An Exception was thrown: {ex.Message}";
            }
           
        }
    }
}
