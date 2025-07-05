using ToDoList.MvcFrontend.Models.AuthModel;

namespace ToDoList.MvcFrontend.Models.Response
{
    public class ApiAuthResponse
    {
        public int StatusCode { get; set; }
        public object Meta { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public object Errors { get; set; }
        public JwtAuthResult Data { get; set; }
    }
}
