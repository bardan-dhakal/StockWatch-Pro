namespace api.Models
{
    public class GenericResponse<T>(T data, bool isSuccess, string message)
    {
        public T Data { get; set; } = data;
        public bool IsSuccess { get; set; } = isSuccess;
        public string Message { get; set; } = message;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public static GenericResponse<T> Success(T data, string message = "")
        {
            return new GenericResponse<T>(data, true, message);
        }

        public static GenericResponse<T?> Failure(string message)
        {
            return new GenericResponse<T?>(default, false, message);
        }
    }
}