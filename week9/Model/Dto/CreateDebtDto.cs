namespace week9.Model.Dto
{
    public class CreateDebtDto
    {
        public Guid Id { get; set; }

        public string DebtSource { get; set; }

        public decimal DebtAmount { get; set; }

        public DateTime DeuDate { get; set; }

        public bool IsCleard { get; set; }

        public bool IsActive { get; set; }

        public List<Tag> Debts { get; set; }
    }
}
