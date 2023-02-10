using MudBlazorUICRUDApp.Shared.AuthModels;

namespace MudBlazorUICRUDApp.Client.Services.Interface
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
        Task<AuthResponseResult> Register(RegisterModel registerModel);
        Task<UserDetails> GetUserInfo();
        Task<AuthResponseResult> ChangePassword(ChangePasswordModel model);
        Task<AuthResponseResult> ResetPassword(ResetPasswordModel model);
    }
}
