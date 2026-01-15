namespace Hyperativa.Api.Models
{
    public class LotDto
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime LotIssueDate { get; set; }
        public int NumberOfRecords { get; set; }
        public string LotCode { get; set; } = string.Empty;
    }
}
