namespace Model.Object;

public class BaseResponseDTO<T>
{
    public bool Status {get; set;} = false;
    public string Message {get;set;} = "";
    public string Code {get;set;} = "";
    public T? Data {get;set;}
}