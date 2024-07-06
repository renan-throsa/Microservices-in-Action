using System.Runtime.Serialization;

namespace ProductCatalog.Utils
{
    public class DBConnectionException : Exception
    {
        public DBConnectionException() : base()
        {
        }
        public DBConnectionException(string message) : base(message)
        {
        }
        public DBConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DBConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
