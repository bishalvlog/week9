using week9.Base;

namespace week9.Model
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public decimal TransactionAmount {  get; set; }

        public DateTime? TransactionDate { get; set; }

        public TransactonType TransactionType { get; set; }

        public bool IsActive { get; set; }

        public string Remarks { get; set; }

        public Guid TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
