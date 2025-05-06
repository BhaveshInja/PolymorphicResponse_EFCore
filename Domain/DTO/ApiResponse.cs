namespace Domain.DTO
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
        public int? StatusCode { get; set; }

        public static ApiResponse<T> Ok(T data, string? message = null)
        {
            return new ApiResponse<T> { Success = true, Data = data, Message = message, StatusCode = 200 };
        }

        public static ApiResponse<T> Created(T data, string? message = null)
        {
            return new ApiResponse<T> { Success = true, Data = data, Message = message, StatusCode = 201 };
        }

        public static ApiResponse<T> Fail(string error, int statusCode = 400, string? message = null)
        {
            return new ApiResponse<T> { Success = false, Errors = new List<string> { error }, Message = message, StatusCode = statusCode };
        }

        public static ApiResponse<T> Fail(List<string> errors, int statusCode = 400, string? message = null)
        {
            return new ApiResponse<T> { Success = false, Errors = errors, Message = message, StatusCode = statusCode };
        }
    }
}
