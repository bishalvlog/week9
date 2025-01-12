using MudBlazor;
using week9.Model;
using week9.Model.Dto;

namespace week9.Components.Pages
{
    public partial class CustomTag
    {
        private List<Tag>? Tags { get; set; }

        #region OnIntialized
        protected override async Task OnInitializedAsync()
        {
            StateHasChanged();
            await GetAllTags();
        }
        #endregion

        #region GetAllTags
        private async Task GetAllTags()
        {
            var response =  UserTag.GetAllTag();

            if (response is null)
            {
               // SnackbarService.ShowSnackbar(response.Message ?? Constant.Message.ExceptionMessage, Severity.Error, Variant.Outlined);
                return;
            }

            Tags = response;

            StateHasChanged();
        }
        #endregion

        #region UpdateTag
        private async Task OpenUpdateTagModal(Guid TagId)
        {
            var response =  UserTag.TagGetById(TagId);

            if (response is null)
            {
               // SnackbarService.ShowSnackbar(response.Message?? Constant.Message.ExceptionMessage, Severity.Error, Variant.Outlined);
            }
        }
        #endregion

        #region AddTag
        private bool IsCreateButtonDisabled =>
        string.IsNullOrEmpty(createTagDto.TagName);

        private bool IsCreateModalOpen { get; set; }

        private CreateTagDto createTagDto { get; set; } = new();

        private void OpenTagRegister()
        {
            IsCreateModalOpen = true;
            createTagDto = new CreateTagDto();
            StateHasChanged();
        }

        private async Task AddRegisterTag(bool isclosed)
        {
            if (isclosed)
            {
               IsCreateModalOpen = false;
                return;
            }

            try
            {
                var result = UserTag.AddTag(createTagDto);

                if(result is null)
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

        private Tag DeleteTags { get; set; } = new();

        private async Task OpenTagDeleteModal(Guid Id)
        {
            var response =  UserTag.TagGetById(Id);

            if (response is null)
            {
               // SnackbarService.ShowSnackbar(response?.Message ?? Constants.Message.ExceptionMessage, Severity.Error, Variant.Outlined);
                return;
            }

            DeleteTags = response;

            IsDeleteModalOpen = true;

            StateHasChanged();
        }

        private async Task DeleteTag(bool isActive)
        {
            try
            {
                UserTag.ActiveDeactive(DeleteTags.Id, isActive);

                IsDeleteModalOpen = false;
            }
            catch (Exception ex)
            {
                //SnackbarService.ShowSnackbar(ex.Message, Severity.Error, Variant.Outlined);
            }
        }
        #endregion

        #region Update CustomTag
        private bool IsUpdateModalOpen { get; set; }

        private UpdateTagDto UpdateTagDto { get; set; } = new();

        private Tag GetTagDto { get; set; } = new();

        private bool IsTagButtonDisabled =>
            string.IsNullOrEmpty(UpdateTagDto.TagName);

        private async Task OpenUpdateOrganizationModal(Guid tagId)
        {
            var response = UserTag.TagGetById(tagId);

            if (response is null)
            { 
                return;
            }

            GetTagDto = response;

            UpdateTagDto = new UpdateTagDto()
            {
                Id = GetTagDto.Id,
                TagName = GetTagDto.TagName,
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
                var result =  UserTag.UpdateTag(UpdateTagDto);

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