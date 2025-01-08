using week9.Abstraction;
using week9.Model;
using week9.Model.Dto;
using week9.Model.Exception;
using week9.Services.Interface;

namespace week9.Services
{
    public class DebtService : UserBase<Debt>, IDebt
    {
        private List<Debt> _debtList;
        public DebtService() : base("Debt.json")
        {
            _debtList = LoadItems();
        }
        public async Task AddDebt(CreateDebtDto debt)
        {
            try
            {
                var debtModel = new Debt()
                {
                    Id = new Guid(),
                    DebtSource = debt.DebtSource,
                    DebtAmount = debt.DebtAmount,
                    DebtDate = DateTime.Now,
                    IsActive = true,
                    IsCleard = false,
                    DeuDate = debt.DeuDate, 
                };

            }
            catch (Exception ex) 
            {
                throw new NotFoundException("some this is wrong");
            }
        }

        public List<Debt> GetAllDebt()
        {
           return _debtList.ToList();
        }

        public Debt GetById(Guid id)
        {
            return _debtList.FirstOrDefault(d => d.Id == id);
        }
    }
}
