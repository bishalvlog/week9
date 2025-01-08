using week9.Model;
using week9.Model.Dto;

namespace week9.Services.Interface
{
    public interface IDebt
    {
        Task AddDebt(CreateDebtDto debt);

        List<Debt> GetAllDebt();

        Debt GetById(Guid id);
    }
}
