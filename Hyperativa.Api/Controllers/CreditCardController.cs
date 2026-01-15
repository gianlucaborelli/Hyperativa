using Hyperativa.Api.Models.Dto;
using Hyperativa.Api.Services;
using Hyperativa.Core.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Hyperativa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CreditCardController(
        ICreditCardService creditCardService,
        ILogger<CreditCardController> logger) : ControllerBase
    {
        private readonly ICreditCardService _creditCardService = creditCardService;
        private readonly ILogger<CreditCardController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetCreditCardByNumber([FromQuery]string creditCardNumber)
        {
            _logger.LogInformation("Iniciando busca de cartão de crédito por número: {CreditCardNumber}", creditCardNumber);
            
            var response = await _creditCardService.GetByCardNumberAsync(creditCardNumber);
            
            _logger.LogInformation("Busca de cartão de crédito finalizada. Sucesso: {Success}", response.Success);
            
            return this.ToActionResult(response);
        }

        [HttpPost]        
        public async Task<IActionResult> CreateCreditCard([FromBody] CreateCreditCardRequest request)
        {
            _logger.LogInformation("Iniciando criação de cartão de crédito");
            
            var response = await _creditCardService.CreateCreditCard(request);
            
            _logger.LogInformation("Criação de cartão de crédito finalizada. Sucesso: {Success}", response.Success);
            
            return this.ToActionResult(response);
        }

        [HttpPost]
        [Route("bulk")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadCreditCardFile([FromForm] IFormFile file)
        {
            _logger.LogInformation("Iniciando upload em lote de cartões de crédito. Nome do arquivo: {FileName}, Tamanho: {FileSize} bytes", 
                file?.FileName, file?.Length);
            
            var response = await _creditCardService.BulkCreateCreditCard(file);
            
            _logger.LogInformation("Upload em lote de cartões de crédito finalizado. Sucesso: {Success}", response.Success);
            
            return this.ToActionResult(response);
        }   
    }
}
