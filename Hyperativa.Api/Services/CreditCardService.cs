using Hyperativa.Api.Data;
using Hyperativa.Api.Helper;
using Hyperativa.Api.Models;
using Hyperativa.Api.Models.Dto;
using Hyperativa.Core.Controller;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Hyperativa.Api.Services
{
    public class CreditCardService (ApplicationContext context) : ICreditCardService
    {
        private readonly ApplicationContext _context = context;

        public async Task<ServiceResponse<CreditCardDto?>> GetByCardNumberAsync(string cardNumber)
        {
            var hash = HashHelper.Hash(cardNumber);

            var creditCard = await _context.CreditCards
                .Include(cc => cc.Lot)
                .FirstOrDefaultAsync(cc => cc.CardNumberHash == hash);

            if (creditCard == null)
                {
                return ServiceResponse<CreditCardDto?>
                    .Fail("Credit Card not found", HttpStatusCode.NotFound);
            }

            var creditCardDto = new CreditCardDto
            {
                Id = creditCard.Id,
                Number = creditCard.CardNumber,
                CreationDate = creditCard.CreatedAt,
                Lot = new LotDto {
                    Id = creditCard.Lot?.Id ?? Guid.Empty,
                    Name = creditCard.Lot?.Name ?? string.Empty,
                    CreationDate = creditCard.Lot?.CreatedAt ?? DateTime.MinValue,
                    LotIssueDate = creditCard.Lot?.LotIssueDate ?? DateTime.MinValue,
                    NumberOfRecords = creditCard.Lot?.NumberOfRecords ?? 0,
                    LotCode = creditCard.Lot?.LotCode ?? string.Empty
                },
                LotPosition = creditCard.LotPosition,
                LotLineIdentifier = creditCard.LotLineIdentifier
            };

            return ServiceResponse<CreditCardDto?>
                .Ok(creditCardDto, "Credit Card retrieved successfully");
        }

        public async Task<ServiceResponse<CreditCardDto>> CreateCreditCard(CreateCreditCardRequest creditCard)
        {
            if(string.IsNullOrEmpty(creditCard.CreditCardNumber))
            {
                return ServiceResponse<CreditCardDto>
                    .Fail("Credit Card Number is required", HttpStatusCode.BadRequest);
            }

            var newCreditCard = new CreditCard
                    (creditCard.CreditCardNumber);
                        
            _context.CreditCards.Add(newCreditCard);
            await _context.SaveChangesAsync();
            var response = new CreditCardDto
            {
                Id = newCreditCard.Id,
                Number = newCreditCard.CardNumber
            };

            return ServiceResponse<CreditCardDto>
                .Ok(response, "Credit Card created successfully");            
        }

        public Task<ServiceResponse<List<CreditCardDto>>> BulkCreateCreditCard(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Task.FromResult(ServiceResponse<List<CreditCardDto>>
                    .Fail("File is required", HttpStatusCode.BadRequest));
            }

            var creditCards = TxtFileReader.ProcessFile(file);

            if (creditCards == null || !creditCards.Any())
            {
                return Task.FromResult(ServiceResponse<List<CreditCardDto>>
                    .Fail("No valid credit card data found in the file", HttpStatusCode.BadRequest));
            }

            _context.CreditCards.AddRange(creditCards);

            return _context.SaveChangesAsync()
                .ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        return ServiceResponse<List<CreditCardDto>>
                            .Fail("Error saving credit cards to the database", HttpStatusCode.InternalServerError);
                    }
                    return ServiceResponse<List<CreditCardDto>>
                        .Ok(creditCards.Select(c => new CreditCardDto
                        {
                            Id = c.Id,
                            Number = c.CardNumber,
                            CreationDate = c.CreatedAt,
                            Lot = new LotDto
                            {
                                Id = c.Lot?.Id ?? Guid.Empty,
                                Name = c.Lot?.Name ?? string.Empty,
                                CreationDate = c.Lot?.CreatedAt ?? DateTime.MinValue,
                                LotIssueDate = c.Lot?.LotIssueDate ?? DateTime.MinValue,
                                NumberOfRecords = c.Lot?.NumberOfRecords ?? 0,
                                LotCode = c.Lot?.LotCode ?? string.Empty
                            },
                            LotPosition = c.LotPosition,
                            LotLineIdentifier = c.LotLineIdentifier
                        }).ToList(), "Credit cards created successfully");
                });
        }
    }
}
