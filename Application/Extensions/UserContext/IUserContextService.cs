namespace Application.Extensions.UserContext
{
    public interface IUserContextService
    {
        int GetCurrentUserId();
        bool IsUserLoggedIn();
    }
}
