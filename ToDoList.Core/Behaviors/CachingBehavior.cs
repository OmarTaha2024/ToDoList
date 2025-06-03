using MediatR;
using ToDoList.Data.Abstract;
using ToDoList.Service.Abstracts;

namespace ToDoList.Core.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly ICacheService _cacheService;

        public CachingBehavior(ICacheService cacheService) { _cacheService = cacheService; }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is not ICacheable cacheable)
                return await next();

            var cached = _cacheService.Get<TResponse>(cacheable.CacheKey);
            if (cached != null)
                return cached;

            var response = await next();
            _cacheService.Set(cacheable.CacheKey, response, cacheable.CacheDuration);

            return response;
        }
    }

}
