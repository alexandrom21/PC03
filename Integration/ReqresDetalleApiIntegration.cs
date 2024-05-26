using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using practica03.Integration.dto;


namespace practica03.Integration
{
    public class ReqresDetalleApiIntegration
    {
        private readonly ILogger<ReqresDetalleApiIntegration> _logger;

        private const string API_URL = "https://reqres.in/api/users/";
        private readonly HttpClient httpClient;

        public ReqresDetalleApiIntegration(ILogger<ReqresDetalleApiIntegration> logger)
        {
            _logger = logger;
            httpClient = new HttpClient();

        }

        public async Task<Registro> GetUser(int Id)
        {

            string requestUrl =  $"{API_URL}{Id}";
            Registro registro = new Registro();
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
                    if (apiResponse != null)
                    {
                        registro = apiResponse.DataRegistro ?? new Registro();
                    }
                }
            }
            catch(Exception ex){
                _logger.LogDebug($"Error al llamar a la API: {ex.Message}");
            }
            return registro;

        }
        class ApiResponse
        {
            public Registro DataRegistro { get; set; }
        }
    }
}