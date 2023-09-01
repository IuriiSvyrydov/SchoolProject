namespace SchoolProject.Core.Bases;

public class ResponseHandler
{
    private IStringLocalizer<SharedResources> _localizer;

    public ResponseHandler(IStringLocalizer<SharedResources> localizer)
    {
        _localizer = localizer;
    }
    public Response<T> Deleted<T>(string message = null)
    {
        return new Response<T>
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Successed = true,
            Message = message == null ? _localizer[SharedResourcesKey.Deleted] : message
        };
    }
    public Response<T> Success<T>(T entity, object meta = null)
    {
        return new Response<T>()
        {
            Data = entity,
            StatusCode = System.Net.HttpStatusCode.OK,
            Successed = true,
            Message = _localizer[SharedResourcesKey.Created],
            Meta = meta

        };
    }
    public Response<T> Unauthorized<T>(string message = null)
    {
        return new Response<T>
        {
            StatusCode = System.Net.HttpStatusCode.Unauthorized,
            Successed = true,
            Message = message == null ? _localizer[SharedResourcesKey.Unauthorized] : message
        };

    }
    public Response<T> BadRequest<T>(string message = null)
    {
        return new Response<T>
        {
            StatusCode = System.Net.HttpStatusCode.BadRequest,

            Successed = false,

            Message = message == null ? _localizer[SharedResourcesKey.BadRequest] : message
        };
    }
    public Response<T> UnprocessableEntity<T>(string message)
    {
        return new Response<T>
        {
            StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,

            Successed = false,

            Message = message == null ? _localizer[SharedResourcesKey.UnproccessableEntity] : message
        };
    }
    public Response<T> NotFound<T>(string message = null)
    {
        return new Response<T>
        {
            StatusCode = System.Net.HttpStatusCode.NotFound,

            Successed = false,

            Message = message == null ? _localizer[SharedResourcesKey.NotFound] : message
        };
    }
    public Response<T> Created<T>(T entity, object meta = null)
    {
        return new Response<T>
        {
            Data = entity,
            StatusCode = System.Net.HttpStatusCode.Created,
            Successed = true,
            Message = _localizer[SharedResourcesKey.Created],
            Meta = meta

        };
    }
}
