using week9.Base;

namespace week9.Model.Dto
{
    public class CreateTransactionDto
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public decimal TransactionAmount { get; set; }

        public DateTime? TransactionDate { get; set; }

        public TransactonType transactionType { get; set; }

        public string Remarks { get; set; }

        public Guid TagId { get; set; }

        public Tag Tage { get; set; }
    }
}
