namespace ProductCatalog.Domain
{
    public class OperationResultModel
    {
        public ResponseStatus Status { get; set; }
        public object? Content { get; set; }
        public bool IsValid => Status == ResponseStatus.Ok || Status == ResponseStatus.Created;
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
