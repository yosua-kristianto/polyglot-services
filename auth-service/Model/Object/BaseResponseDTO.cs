namespace Model.Object;

public class BaseResponseDTO<T>
{
    public bool Status {get; set;} = false;
    public string Message {get;set;} = "";
    public string Code {get;set;} = "";
    public T? Data {get;set;}

    public static BaseResponseDTO<T> Success(
        T data,
        string message = "Success",
        string code = "200")
    {
        return new BaseResponseDTO<T>
        {
            Status = true,
            Message = message,
            Code = code,
            Data = data
        };
    }

    public static BaseResponseDTO<T> Error(
        string message = "Failed",
        string code = "500")
    {
        return new BaseResponseDTO<T>
        {
            Status = false,
            Message = message,
            Code = code,
            Data = default
        };
    }
}