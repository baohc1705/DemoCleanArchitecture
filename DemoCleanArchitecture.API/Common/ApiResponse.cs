namespace DemoCleanArchitecture.API.Common
{
    public class ApiResponse<T>
    {
        public int Code { get; set; } = 1000;
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }


        public static ApiResponse<T> Ok(T data, string message = "Success", int code = 1000)
            => new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                Code = code
            };

        public static ApiResponse<T> Fail(string message, int code = 1001, List<string>? errors = null)
            => new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Code = code,
                Errors = errors
            };
    }
}
