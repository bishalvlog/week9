using week9.Abstraction;
using week9.Base;
using week9.Model;
using week9.Model.Dto;
using week9.Model.Exception;
using week9.Services.Interface;

namespace week9.Services
{
    public class DebtService : UserBase<Debt>, IDebt
    {
        private List<Debt> _debtList;

        private readonly ITransaction _transaction;
        public DebtService(ITransaction transaction) : base("Debt.json")
        {
            _debtList = LoadItems();
            _transaction = transaction;
        }

        public void ActiveDeactive(Guid Id)
        {
            UpdateItem(t => t.Id == Id, t =>
            {
                t.IsActive = false;
            });
        }

        public async Task AddDebt(CreateDebtDto debt)
        {
            try
            {
                var debtModel = new Debt()
                {
                    Id = Guid.NewGuid(),
                    DebtSource = debt.DebtSource,
                    DebtAmount = debt.DebtAmount,
                    DebtDate = DateTime.Now,
                    IsActive = true,
                    IsCleard = false,
                    DueDate = debt.DueDate, 
                    TagId = debt.TagId,
                };

                _debtList.Add(debtModel);
                SaveItems(_debtList);

                var transaction = new CreateTransactionDto
                {
                    Title = $"debt Add : {debt.DebtSource}",
                    TransactionAmount = debt.DebtAmount,
                    TransactionDate = DateTime.Now,
                    TransactionType = (int)TransactonType.Debt,
                    TagId = debt.TagId,
                    Remarks = "Add Debts "
                };

                await _transaction.AddTransaction(transaction);
            }
            catch (Exception ex) 
            {
                throw new NotFoundException("some this is wrong");
            }
        }

        public List<Debt> GetAllDebt()
        {
           return _debtList.Where(t => t.IsActive).ToList();
        }

        public Debt GetById(Guid id)
        {
            return _debtList.FirstOrDefault(d => d.Id == id);
        }

        public async Task UpdateDebt(UpdateDebtDto debt)
        {
            UpdateItem(t => t.Id == debt.Id, t =>
            {
                t.DebtSource = debt.DebtSource;
                t.DebtDate = debt.DebtDate;
                t.DebtAmount = debt.DebtAmount;
            });
        }
    }
}
