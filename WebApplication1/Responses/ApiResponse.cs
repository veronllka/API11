namespace WebApplication1.Responses
{
    public class ApiError
    {
        public int Code { get; set; }
        public string Message { get; set; } = "";
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? TraceId { get; set; }
        public T? Data { get; set; }
        public ApiError? Error { get; set; }

        public static ApiResponse<T> Ok(T data, string? traceId = null) =>
            new ApiResponse<T> { Success = true, Data = data, TraceId = traceId };

        public static ApiResponse<T> Fail(int code, string message, string? traceId = null) =>
            new ApiResponse<T>
            {
                Success = false,
                TraceId = traceId,
                Error = new ApiError { Code = code, Message = message }
            };
    }
}
