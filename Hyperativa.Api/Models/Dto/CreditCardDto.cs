namespace Hyperativa.Api.Models.Dto
{
    public class CreditCardDto
    {
        public Guid Id { get; set; }

        private string _number = string.Empty;

        public string Number
        {
            get => _number;
            set => _number = Mask(value);
        }

        public DateTime CreationDate { get; set; }
        public LotDto? Lot { get; set; }
        public string? LotPosition { get; set; }
        public string? LotLineIdentifier { get; set; }

        private static string Mask(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                return string.Empty;

            // Remove espaços e hífens
            number = number.Replace(" ", "").Replace("-", "");

            if (number.Length <= 4)
                return number;

            var last4 = number[^4..];
            return new string('*', number.Length - 4) + last4;
        }
    }
}
