
using week9.Model;
using week9.Model.Dto;

namespace week9.Components.Pages
{
    public partial class AddTransaction
    {
        private List<Transaction>? transactions { get; set; }

        #region OnInitialize
        protected override async Task OnInitializedAsync()
        {
            await GetAllTransaction();
            await GetAllTags();
        }
        #endregion

        private async Task OpenUpdateDebtModal(Guid TagId)
        {
            var response = UserDebt.GetById(TagId);

            if (response is null)
            {
                // SnackbarService.ShowSnackbar(response.Message?? Constant.Message.ExceptionMessage, Severity.Error, Variant.Outlined);
            }
        }

        #region GetAllTransaction
        private async Task GetAllTransaction()
        {
            var response = UserTransaction.GetAllTransaction();

            if (response == null)
            {
                return;
            }

            transactions = response;
        }
        #endregion

        #region Add Transaction
        private bool IsCreateButtonDisabled =>
        string.IsNullOrEmpty(createTransaction.Title) ||
        string.IsNullOrEmpty(createTransaction.TransactionDate.ToString()) ||
        string.IsNullOrEmpty(createTransaction.TransactionType.ToString()) ||
        string.IsNullOrEmpty(createTransaction.Remarks);

        private bool IsCreateModalOpen { get; set; }

        private CreateTransactionDto createTransaction { get; set; } = new();

        private void OpenTransactionRegister()
        {
            IsCreateModalOpen = true;
            createTransaction = new CreateTransactionDto();
            StateHasChanged();
        }

        private async Task AddRegisterTransaction(bool isclosed)
        {
            if (isclosed)
            {
                IsCreateModalOpen = false;
                return;
            }

            try
            {
                var result = UserTransaction.AddTransaction(createTransaction);

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

        #region GetAll Tags
        private List<Tag>? Tags { get; set; }
        private async Task GetAllTags()
        {
            var response = UserTag.GetAllTag();

            if (response is null)
            {
                return;
            }

            Tags = response;

            StateHasChanged();
        }
        #endregion

        #region Delete
        private bool IsDeleteModalOpen { get; set; }

        private Transaction DeleteTransaction { get; set; } = new();

        private async Task OpenDebtDeleteModal(Guid Id)
        {
            var response = UserTransaction.TransactionGetById(Id);

            if (response is null)
            {
                // SnackbarService.ShowSnackbar(response?.Message ?? Constants.Message.ExceptionMessage, Severity.Error, Variant.Outlined);
                return;
            }

            DeleteTransaction = response;

            IsDeleteModalOpen = true;

            StateHasChanged();
        }

        private async Task DeleteTrans(bool isActive)
        {
            try
            {
                UserTransaction.ActiveDeactive(DeleteTransaction.Id, isActive);

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