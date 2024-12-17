namespace BlazorFrontend.Services
{
    public class AuthState
    {
        public bool IsLoggedIn { get; private set; } = false;
        public void LogIn() => IsLoggedIn = true;
        public void LogOut() => IsLoggedIn = false;
    }
}
