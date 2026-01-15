using Hyperativa.Core.Models;

namespace Hyperativa.Api.Models
{
    public class Lot: Entity
    {
        public string Name { get; set; } = string.Empty;
        public DateTime LotIssueDate { get; set; }
        public int NumberOfRecords { get; set; }
        public string LotCode { get; set; } = string.Empty;

        public List<CreditCard> CreditCards { get; set; } = new List<CreditCard>();
    }
}
