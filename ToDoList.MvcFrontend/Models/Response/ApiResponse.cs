namespace ToDoList.MvcFrontend.Models.Response
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public Meta Meta { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public object Errors { get; set; }
        public T Data { get; set; }
    }

    public class Meta
    {
        public int Count { get; set; }
    }

}
