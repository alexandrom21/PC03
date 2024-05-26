using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using practica03.Integration.dto;

namespace practica03.Integration
{
    public class ReqresListaApiIntegration
    {
        private readonly ILogger<ReqresListaApiIntegration> _logger;

        private const string API_URL = "https://reqres.in/api/users";
        private readonly HttpClient httpClient;

        public ReqresListaApiIntegration(ILogger<ReqresListaApiIntegration> logger)
        {
            _logger = logger;
            httpClient = new HttpClient();

        }

        public async Task<List<Registro>> GetAllUser()
        {

            string requestUrl = API_URL;
            List<Registro> lista = new List<Registro>();
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
                    if (apiResponse != null)
                    {
                        lista = apiResponse.DataRegistro ?? new List<Registro>();
                    }
                }
            }
            catch(Exception ex){
                _logger.LogDebug($"Error al llamar a la API: {ex.Message}");
            }
            return lista;

        }
        class ApiResponse
        {
            public int Page { get; set; }
            public int PerPage { get; set; }
            public int Total { get; set; }
            public int TotalPages { get; set; }
            public List<Registro> DataRegistro { get; set; }
        }

    }
}