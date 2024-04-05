namespace Domain.Result;

public class BaseResult
{
    public bool IsSuccess => ErrorMessage == null;

    public string ErrorMessage { get; set; }

}

public class BaseResult<T> : BaseResult
{
    public BaseResult(string errorMessege, T data)
    {
        ErrorMessage = errorMessege;
        Data = data;
    }

    public BaseResult() { }
    public T Data { get; set; }
}
