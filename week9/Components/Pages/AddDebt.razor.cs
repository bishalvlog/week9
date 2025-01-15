using week9.Model;
using week9.Model.Dto;

namespace week9.Components.Pages
{
    public partial class AddDebt
    {
        private List<Debt>? debts { get; set; } = new();

        #region Oninitialize
        protected override async Task OnInitializedAsync()
        {
            await GetAllDebt();
            await GetAllTags();
            StateHasChanged();
        }
        #endregion

        #region GetAllDebt
        private async Task GetAllDebt()
        {
            var response = UserDebt.GetAllDebt();

            if (response is null)
            {
                return;
            }

             debts = response;

            StateHasChanged();
        }
        #endregion

        #region GetAll Tags
        private List<Tag>? Tags { get; set; }
        private async Task GetAllTags()
        {
            var response = UserTag.GetAllTagUseByOther();

            if(response is null)
            {
                return;
            }

            Tags = response;

            StateHasChanged();
        }
        #endregion

        #region AddDebt
        private bool IsCreateButtonDisabled =>
        string.IsNullOrEmpty(CreateDebtDto.DebtSource) || 
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

                IsCreateModalOpen = false;
                StateHasChanged();
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

        private async Task OpenDebtDeleteModal(Guid Id)
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
                UserDebt.ActiveDeactive(DeleteDebt.Id);

                IsDeleteModalOpen = false;
            }
            catch (Exception ex)
            {
                //SnackbarService.ShowSnackbar(ex.Message, Severity.Error, Variant.Outlined);
            }
        }
        #endregion

        #region Update Debts
        private bool IsUpdateModalOpen { get; set; }

        private UpdateDebtDto UpdateDebtDto { get; set; } = new();

        private Debt GetDebtDto { get; set; } = new();

        private bool IsDebtButtonDisabled =>
            string.IsNullOrEmpty(UpdateDebtDto.DebtSource) ||
            string.IsNullOrEmpty(UpdateDebtDto.DueDate.ToString()) ||
            string.IsNullOrEmpty(UpdateDebtDto.DebtAmount.ToString()) ||
            string.IsNullOrEmpty(UpdateDebtDto.Tag?.TagName);

        private async Task OpenUpdateModal(Guid debtId)
        {
            var response = UserDebt.GetById(debtId);

            if (response is null)
            {
                return;
            }

            GetDebtDto = response;

            UpdateDebtDto = new UpdateDebtDto()
            {
                Id = GetDebtDto.Id,
                DebtSource = GetDebtDto.DebtSource,
                DebtDate = GetDebtDto.DebtDate,
                DebtAmount = GetDebtDto.DebtAmount
            };

            OpenCloseEditModal();
            StateHasChanged();
        }

        private void OpenCloseEditModal()
        {
            IsUpdateModalOpen = !IsUpdateModalOpen;

            StateHasChanged();
        }

        private async Task UpdateTag(bool isClosed)
        {
            if (isClosed)
            {
                IsUpdateModalOpen = false;
                return;
            }

            try
            {
                var result = UserDebt.UpdateDebt(UpdateDebtDto);

                if (result is null)
                {
                    //SnackbarService.ShowSnackbar(result?.Message ?? Constants.Message.ExceptionMessage, Severity.Error, Variant.Outlined);
                    return;
                }
            }
            catch (Exception ex)
            {
                // SnackbarService.ShowSnackbar(ex.Message, Severity.Error, Variant.Outlined);
            }
        }
        #endregion
    }
}