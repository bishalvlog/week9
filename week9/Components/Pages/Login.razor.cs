using week9.Model;

namespace week9.Components.Pages
{
    public partial class Login
    {

        private User Users { get; set; } = new();

        private string ErrorMessage { get; set; } = string.Empty;


        private void HandleLogin()
        {
            if (UserService.Login(Users))
            {
                Nav.NavigateTo("/home");
            }

            else
            {
                ErrorMessage = "userName or password is invalid";
            }
        }

    }
}