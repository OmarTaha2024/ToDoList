using ToDoList.Data.Results;

namespace ToDoList.Service.Abstracts
{
    public interface IOAuthService
    {
        public Task<string> GetAuthorizationUrl();
        public Task<OAuthCallBack> GetAuthorizationCallBack(string code);
    }
}
