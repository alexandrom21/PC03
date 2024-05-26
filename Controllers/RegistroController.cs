using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using practica03.Integration;
using practica03.Integration.dto;

namespace practica03.Controllers
{
    public class RegistroController : Controller
    {
        private readonly ILogger<RegistroController> _logger;
        private readonly ReqresRegistroApiIntegration _reqresRegistroApiIntegration;


        public RegistroController(ILogger<RegistroController> logger,
            ReqresRegistroApiIntegration reqresRegistroApiIntegration)
        {
            _logger = logger;
            _reqresRegistroApiIntegration = reqresRegistroApiIntegration ;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, string job)
        {
            try
            {
                var response = await _reqresRegistroApiIntegration.CreateUser(name, job);
                
                if (response != null)
                {
                    TempData["SuccessMessage"] = "Usuario "+response.Name+" creado correctamente";
                }
                else
                {
                    ModelState.AddModelError("", "Error al crear el usuario");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el usuario: {ex.Message}");
                ModelState.AddModelError("", "Error al crear el usuario");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}