namespace FastFood.Application.UseCases
{
    public class UseCaseResult
    {
        public bool IsSuccess { get; protected set; }
        public string Message { get; protected set; }

        public static UseCaseResult Success() => new UseCaseResult { IsSuccess = true };
        public static UseCaseResult Success(string message) => new UseCaseResult { IsSuccess = true, Message = message };
        public static UseCaseResult Failure(string message) => new UseCaseResult { IsSuccess = false, Message = message };
    }

    public class UseCaseResult<T> : UseCaseResult
    {
        public T Data { get; protected set; }
        public static UseCaseResult<T> Success(T data) => new UseCaseResult<T> { IsSuccess = true, Data = data };
        public static UseCaseResult<T> Success(T data, string message) => new UseCaseResult<T> { IsSuccess = true, Data = data, Message = message };
        public static UseCaseResult<T> Failure(string message) => new UseCaseResult<T> { IsSuccess = false, Message = message };
    }
}
