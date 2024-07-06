namespace ProductCatalog.Services
{
    public class Response
    {
        public ResponseStatus Status { get; set; }
        public object? Payload { get; set; }

    }

    public enum ResponseStatus
    {
        Ok = 200,
        Created = 201,
        Found = 302,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        Conflict = 409,
    }
}
