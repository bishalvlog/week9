using week9.Model;

namespace week9.Components.Pages
{
    public partial class AddDebt
    {
        private List<Debt> debts {  get; set; }


        private async Task OpenUpdateDebtModal(Guid TagId)
        {
            var response = UserTag.TagGetById(TagId);

            if (response is null)
            {
                // SnackbarService.ShowSnackbar(response.Message?? Constant.Message.ExceptionMessage, Severity.Error, Variant.Outlined);
            }
        }

    }
}