using week9.Model;
using week9.Model.Dto;

namespace week9.Components.Pages
{
    public partial class AddDebt
    {
        private List<Debt> debts {  get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetAllDebt();
        }

        private async Task OpenUpdateDebtModal(Guid TagId)
        {
            var response = UserDebt.GetById(TagId);

            if (response is null)
            {
                // SnackbarService.ShowSnackbar(response.Message?? Constant.Message.ExceptionMessage, Severity.Error, Variant.Outlined);
            }
        }

        #region GetAllDebt
        private async Task GetAllDebt()
        {
            var response = UserDebt.GetAllDebt();

            if (response is null)
            {

            }

            response = debts;
        }
        #endregion

        #region AddDebt

        private bool IsCreateButtonDisabled =>
        string.IsNullOrEmpty(CreateDebtDto.DebtSource) ||
        string.IsNullOrEmpty(CreateDebtDto.DeuDate.ToString()) ||
        string.IsNullOrEmpty(CreateDebtDto.DebtAmount.ToString());
           

        private CreateDebtDto CreateDebtDto {  get; set; } = new();
        private bool IsCreateModalOpen { get; set; }    
        private void OpenDebtRegister()
        {
            IsCreateModalOpen = true;
            CreateDebtDto = new CreateDebtDto();
            StateHasChanged();
        }

        private async Task AddRegisterDebt(bool isclosed)
        {
            if (isclosed)
            {
                IsCreateModalOpen = false;
                return;
            }

            try
            {
                var result =  UserDebt.AddDebt(CreateDebtDto);

                if (result is null)
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("");
            }
        }
        #endregion

        #region Delete
        private bool IsDeleteModalOpen { get; set; }

        private Debt DeleteDebt { get; set; } = new();

        private async Task OpenTagDeleteModal(Guid Id)
        {
            var response = UserDebt.GetById(Id);

            if (response is null)
            {
                // SnackbarService.ShowSnackbar(response?.Message ?? Constants.Message.ExceptionMessage, Severity.Error, Variant.Outlined);
                return;
            }

            DeleteDebt = response;

            IsDeleteModalOpen = true;

            StateHasChanged();
        }

        private async Task DeleteTag(bool isClosed)
        {
            if (isClosed)
            {
                IsDeleteModalOpen = false;
                return;
            }

            try
            {
                UserTag.ActiveDeactive(DeleteDebt.Id);

                IsDeleteModalOpen = false;
            }
            catch (Exception ex)
            {
                //SnackbarService.ShowSnackbar(ex.Message, Severity.Error, Variant.Outlined);
            }
        }
      #endregion
    }
}