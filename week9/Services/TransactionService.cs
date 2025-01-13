using week9.Abstraction;
using week9.Base;
using week9.Model;
using week9.Model.Dto;
using week9.Model.Exception;
using week9.Services.Interface;

namespace week9.Services
{
    public class TransactionService : UserBase<Transaction>, ITransaction
    {
        private List<Transaction> _transactions;

        public TransactionService()  : base("Transaction.json")
        {
            _transactions = LoadItems();
        }

        public void ActiveDeactive(Guid Id, bool isActive)
        {
            var success = UpdateItem(t => t.Id == Id, t => t.IsActive = isActive);
        }

        public async Task AddTransaction(CreateTransactionDto createTransaction)
        {
            try
            {
                var modelTransaction = new Transaction()
                {
                    Id = Guid.NewGuid(),
                    Title = createTransaction.Title,
                    TransactionAmount = createTransaction.TransactionAmount,
                    TransactionDate = DateTime.Now,
                    IsActive = true,
                    Remarks = createTransaction.Remarks,
                    TagId = createTransaction.TagId,
                    TransactionType = createTransaction.TransactionType,
                };

                _transactions.Add(modelTransaction);

                SaveItems(_transactions);
            }
            catch (Exception ex)
            {
                throw new NotFoundException("some this is wrong");
            }
        }

        public async Task<Decimal> CurrentBalance()
        {
            var transaction = GetAllTransaction();

            var totalCredit = transaction.Where(t => t.TransactionType == 4)           
                             .Sum(t => t.TransactionAmount);

            var totalDebit = transaction.Where(t => t.TransactionType == 5)
                            .Sum(t => t.TransactionAmount);

            var totalDebt = transaction.Where(t => t.TransactionType == 6).Sum(t => t.TransactionAmount);

            var sumofTransaction = totalCredit - totalDebit;

            var currentBalance = sumofTransaction + totalDebt;

            return currentBalance;
        }

        public List<Transaction> GetAllTransaction()
        {
           return _transactions.ToList();
        }

        public async Task<List<Transaction>> HighestTransaction()
        {
            var transaction =  GetAllTransaction();
            return transaction.OrderByDescending(t => t.TransactionAmount).Take(5).ToList();
        }

        public Transaction TransactionGetById(Guid Id)
        {
            return _transactions.FirstOrDefault(t => t.Id == Id);
        }
    }
}
