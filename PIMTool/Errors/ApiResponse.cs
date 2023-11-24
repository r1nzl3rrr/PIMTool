using System.Diagnostics;

namespace PIMTool.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Request processed successfully",
                400 => "You have made a Bad Request",
                401 => "You are not Authorized",
                404 => "No resource was found",
                500 => "Server Error",
                _ => null
            };
        }
    }
}
