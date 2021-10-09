namespace Api.Infrastructure.Constants
{
    public static class ApiErrors
    {
        public const string ERROR_INVALID = "Invalid username or password";
        public const string ERROR_USER_EXISTS = "User with this phone already exists";
        public const string ERROR_USER_NOT_CREATED = "User wasn't created";
        public const string ERROR_ROLE = "Invalid role";

        public const string ERROR_OBJECTIVE_NOT_CREATED = "Objective wasn't created";
        public const string ERROR_OBJECTIVE_NOT_UPDATED = "Objective wasn't updated";
    }
}
