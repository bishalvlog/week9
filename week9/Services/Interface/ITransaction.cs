using week9.Model;
using week9.Model.Dto;

namespace week9.Services.Interface
{
    public interface ITransaction
    {
        List<Transaction> GetAllTransaction();

        Transaction TransactionGetById(Guid Id);

        Task AddTransaction(CreateTransactionDto createTransaction);

        void ActiveDeactive(Guid Id);
    }
}
