using week9.Abstraction;
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

        public List<Transaction> GetAllTransaction()
        {
           return _transactions.ToList();
        }

        public Transaction TransactionGetById(Guid Id)
        {
            return _transactions.FirstOrDefault(t => t.Id == Id);
        }
    }
}
