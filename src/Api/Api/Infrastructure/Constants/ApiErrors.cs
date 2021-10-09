namespace Api.Infrastructure.Constants
{
    public static class ApiErrors
    {
        public const string INVALID = "Invalid username or password";
        public const string USER_EXISTS = "User with this phone already exists";
        public const string INVALID_ROLE = "Invalid role";

        public const string NOT_FOUND = "Not found";
        public const string FAIL_UPDATE = "Update failed";
        public const string FAIL_CREATE = "Create failed";
    }
}
