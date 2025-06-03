namespace ToDoList.Data.Abstract
{
    public interface ICacheable
    {
        string CacheKey { get; }
        TimeSpan CacheDuration { get; }
    }

}
