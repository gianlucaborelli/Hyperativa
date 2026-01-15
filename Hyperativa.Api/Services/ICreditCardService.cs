using Hyperativa.Api.Models;
using Hyperativa.Api.Models.Dto;
using Hyperativa.Core.Controller;

namespace Hyperativa.Api.Services
{
    public interface ICreditCardService
    {
        Task<ServiceResponse<CreditCardDto?>> GetByCardNumberAsync(string cardNumber);
        Task<ServiceResponse<CreditCardDto>> CreateCreditCard(CreateCreditCardRequest creditCard);
        Task<ServiceResponse<List<CreditCardDto>>> BulkCreateCreditCard(IFormFile file);
    }
}
