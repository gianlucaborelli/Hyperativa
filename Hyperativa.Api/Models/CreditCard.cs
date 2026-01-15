using Hyperativa.Api.Helper;
using Hyperativa.Core.Models;

namespace Hyperativa.Api.Models
{
    public class CreditCard : Entity
    {
        public string CardNumber { get; private set; } = null!;
        public string CardNumberHash { get; private set; } = null!;


        public Lot? Lot { get; set; }
        public Guid? LotId { get; set; }
        public string? LotPosition { get; set; }
        public string? LotLineIdentifier { get; set; }

        public CreditCard(string cardNumber)
        {
            SetCardNumber(cardNumber);
        }

        public CreditCard(string cardNumber, Lot? lot, string? lotPosition, string? lotLineIdentifier)
        {
            SetCardNumber(cardNumber);
            Lot = lot;
            LotPosition = lotPosition;
            LotLineIdentifier = lotLineIdentifier;
        }

        public void SetCardNumber(string cardNumber)
        {
            CardNumber = cardNumber;
            CardNumberHash = HashHelper.Hash(cardNumber);
        }
    }
}
