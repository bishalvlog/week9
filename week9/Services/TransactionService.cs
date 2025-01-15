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
            return _transactions.Where(t => t.IsActive).OrderByDescending(t => t.Id).ToList();
        }

        public async Task<List<Transaction>> HighestTransaction()
        {
            var transaction =  GetAllTransaction();
            return transaction.OrderByDescending(t => t.TransactionAmount).Take(5).ToList();
        }

        public async Task<List<Transaction>> SearchUser(FilterDto filterDto)
        {
            try
            {
                var query = _transactions.AsQueryable();

                if (!string.IsNullOrWhiteSpace(filterDto.Title))
                {
                    query = query.Where(t => t.Title != null &&
                                    t.Title.Contains(filterDto.Title, StringComparison.OrdinalIgnoreCase));
                }

                if (filterDto.TransactionDate.HasValue)
                {
                    query = query.Where(t => t.TransactionDate.HasValue && t.TransactionDate.Value.Date == filterDto.TransactionDate.Value.Date);
                }

                if(filterDto.StartDate.HasValue && filterDto.EndDate.HasValue)
                {
                    query = query.Where(t => t.TransactionDate.HasValue && t.TransactionDate.Value.Date >= filterDto.StartDate.Value.Date &&
                                        t.TransactionDate.Value.Date <= filterDto.EndDate.Value.Date);
                }

                var result = query.ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw new NotFoundException("An error occurred while searching for the user.");
            }
        }

        public Transaction TransactionGetById(Guid Id)
        {
            return _transactions.FirstOrDefault(t => t.Id == Id);
        }

        public async Task UpdateTransaction(UpdateTransactionDto updateTransactionDto)
        {
            UpdateItem(t => t.Id == updateTransactionDto.Id, t =>
            {
                t.Title = updateTransactionDto.Title;
                t.TransactionAmount = updateTransactionDto.TransactionAmount;
                t.TransactionDate = updateTransactionDto.TransactionDate;
                t.TransactionType = updateTransactionDto.TransactionType;
                t.Remarks = updateTransactionDto.Remarks;
            });
        }
    }
}
