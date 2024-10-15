namespace Clothing.Application.DTOs.AuthenticationDTO
{
    public class LoginResponseDto
    {
        internal LoginResponseDto
       (
           bool isLoginSuccess,
           string token,
           string displayName,
           int userId,
           string message,
           string? routeURL
       )
        {
            IsLoginSuccess = isLoginSuccess;
            Token = token;
            DisplayName = displayName;
            UserId = userId;
            Message = message;
            RouteURL = routeURL;
        }
        public bool IsLoginSuccess { get; set; }
        public string Token { get; set; }
        public string DisplayName { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public string? RouteURL { get; set; }

        public static LoginResponseDto NotSuccess(string errorMessage)
        {
            return new LoginResponseDto
                       (
                            false,
                            string.Empty,
                            string.Empty,
                            0,
                            errorMessage,
                            string.Empty
                       );
        }

        public static LoginResponseDto Success
                      (
                        string token,
                        string displayName,
                        int userId,
                       string? routeURL
                      )
        {
            return new LoginResponseDto
                       (
                            true,
                            token,
                            displayName,
                            userId,
                            "User login is successfully.",
                            routeURL
                       );
        }
    }
}
