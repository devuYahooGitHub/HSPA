using System.Text.Json;

namespace WebApi.Errors
{
    public class ApiError
    {
        public ApiError(int errorCode, string errorMessage, string errorDetails=null)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
            this.ErrorDetails = errorDetails;

        }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}