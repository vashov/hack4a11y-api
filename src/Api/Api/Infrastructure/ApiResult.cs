namespace Api.Infrastructure
{
    public class ApiResult<T>
    {
        public static ApiResult<T> Error(string errorMsg)
        {
            return new ApiResult<T>
            {
                Result = default,
                ErrorMsg = errorMsg
            };
        }

        public static ApiResult<T> Ok(T result)
        {
            return new ApiResult<T>
            {
                Result = result,
                ErrorMsg = null
            };
        }

        private ApiResult() { }

        public T Result { get; set; }
        public string ErrorMsg { get; set; }
        public bool IsOk => ErrorMsg == null;
    }

    public class ApiResult
    {
        public static ApiResult Error(string errorMsg)
        {
            return new ApiResult
            {
                ErrorMsg = errorMsg
            };
        }

        public static ApiResult Ok()
        {
            return new ApiResult
            {
                ErrorMsg = null
            };
        }

        private ApiResult() { }

        public string ErrorMsg { get; set; }
        public bool IsOk => ErrorMsg == null;
    }
}
