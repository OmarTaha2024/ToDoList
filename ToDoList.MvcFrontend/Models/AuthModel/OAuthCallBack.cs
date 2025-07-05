namespace ToDoList.MvcFrontend.Models.AuthModel
{
    public class OAuthCallBack
    {
        public string AccessToken { get; set; }
        public DateTime Expirein { get; set; }
        public string Scope { get; set; }
        public string Tokentype { get; set; }
        public string Id_Token { get; set; }
    }
}
