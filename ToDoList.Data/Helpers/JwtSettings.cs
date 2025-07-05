namespace ToDoList.Data.Helpers
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessTokenExpireDate { get; set; }
        public int RefreshTokenExpireDate { get; set; }
    }

}
