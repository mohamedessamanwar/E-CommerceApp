using System.Net;

namespace BusinessAccessLayer.DTOS.Response
{
    // var<t> mmm = new var<t>(mmmmmm) ;
    public class Response<T> where T : class
    {
        public Response()
        {

        }
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public Response(string message, bool succeeded)
        {
            Succeeded = succeeded;
            Message = message;
        }

        public HttpStatusCode StatusCode { get; set; }
        public object Meta { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public Dictionary<string, List<string>> ErrorsDic { get; set; }
        public T Data { get; set; }
    }
}
