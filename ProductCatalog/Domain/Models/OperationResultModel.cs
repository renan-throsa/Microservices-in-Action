using System.Net;

namespace ProductCatalog.Domain.Models
{
    public class OperationResultModel
    {
        public HttpStatusCode Status { get; set; }
        public object? Content { get; set; }
        public bool IsValid => Status == HttpStatusCode.OK || Status == HttpStatusCode.Created || Status == HttpStatusCode.Found;
    }

}
