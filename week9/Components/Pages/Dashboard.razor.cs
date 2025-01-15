using week9.Model;

namespace week9.Components.Pages
{
    public partial class Dashboard
    {
        #region Oninilization

        protected override async Task OnInitializedAsync()
        {
            await Highest();
            await CurrentAmount();
            await PendingDebts();
        }
        #endregion

        #region Highest Top 5 

        private List<Transaction> transactions { get; set; } = [];
        private async Task Highest()
        {
            var response = await UserTransaction.HighestTransaction();

            if (response is null)
            {
                return;
            }

            transactions = response;
        }
        #endregion

        #region Current Balance
        private decimal CurrentBalance { get; set; }

        private async Task CurrentAmount()
        {
            var response = await UserTransaction.CurrentBalance();

            if(response <=0)
            {
                return;
            }

            CurrentBalance = response;
        }
        #endregion

        #region PendingDebts

        private List<Debt>? RemainingDebt { get; set; } = [];
        private async Task PendingDebts()
        {
            var response = await UserDebt.RemainingDebt();

            if (response is null) 
            {
                return;
            }

            RemainingDebt = response;
        }
        #endregion
    }
}