using Microsoft.Extensions.Localization;
using ToDoList.Core.Resources;

namespace ToDoList.Core.Bases
{
    public class ResponseHandler
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public ResponseHandler(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }
        public Response<T> Deleted<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = message == null ? _stringLocalizer[SharedResourcesKeys.Deleted] : message
            };
        }
        public Response<T> Success<T>(T entity, string message = null, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = message == null ? _stringLocalizer[SharedResourcesKeys.Success] : message,
                Meta = Meta
            };
        }

        public Response<T> BadRequest<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = Message == null ? _stringLocalizer[SharedResourcesKeys.BadRequest] : Message
            };
        }
        public Response<T> UnprocessableEntity<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = Message == null ? _stringLocalizer[SharedResourcesKeys.UnprocessableEntity] : Message
            };
        }



        public Response<T> Created<T>(T entity, string message = null, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = message == null ? _stringLocalizer[SharedResourcesKeys.Created] : message,
                Meta = Meta
            };
        }
    }
}
