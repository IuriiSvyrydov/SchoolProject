

using System.Net;

namespace SchoolProject.Core.Bases;

public class Response<T>
{

    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public object Meta { get; set; }
    public T Data { get; set; }
    public List<string> Errors { get; set; }
    public bool Successed { get; set; }
    public Response()
    {

    }
    public Response(T data, string message)
    {
        Successed = true;
        Message = message;
        Data = data;
    }
    public Response(string message)
    {
        Successed = false;
        Message = message;
    }
    public Response(string message, bool successded)
    {
        Successed = successded;
        Message = message;
    }
}
