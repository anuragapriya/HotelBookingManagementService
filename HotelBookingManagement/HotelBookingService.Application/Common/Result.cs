namespace HotelBookingService.HotelBookingService.Application.Common
{
    public class Result<T>
    {
        public bool Success { get; private set; }
        public T? Data { get; private set; }
        public string? ErrorMessage { get; private set; }

        public static Result<T> Ok(T data) => new() { Success = true, Data = data };
        public static Result<T> Fail(string errorMessage) => new() { Success = false, ErrorMessage = errorMessage };
    }

}
