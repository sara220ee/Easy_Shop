



namespace EasyShopWindow
{
    public class LoginResult
    {
        public bool IsSuccess { get; }
        public string Message { get; }
        public bool IsAdmin { get; }

        public LoginResult(bool isSuccess, string message, bool isAdmin = false)
        {
            IsSuccess = isSuccess;
            Message = message;
            IsAdmin = isAdmin;
        }
    }
}
