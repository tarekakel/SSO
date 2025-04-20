using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.General.Dtos
{
    public class GeneralResponse<T>
    {
        public bool Success { get; set; }            // Indicates success or failure
        public string Message { get; set; }          // Message for the client (success, error details, etc.)
        public T Result { get; set; }                // The actual data returned
        public DateTime Timestamp { get; set; }      // Time of response generation
        public string TraceId { get; set; }          // Optional: Correlation ID for logging/tracking
        public List<string> Errors { get; set; }     // Optional: List of error messages, useful for validation errors

        public GeneralResponse()
        {
            Timestamp = DateTime.UtcNow;
            Errors = new List<string>();
        }

        public static GeneralResponse<T> SuccessResponse(T result, string message = "Request successful")
        {
            return new GeneralResponse<T>
            {
                Success = true,
                Message = message,
                Result = result
            };
        }

        public static GeneralResponse<T> ErrorResponse(string message, List<string> errors = null)
        {
            return new GeneralResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }



    }


    public class PagedResult<T>
    {
        public int TotalCount { get; set; }           // Total records
        public int PageIndex { get; set; }            // Current page
        public int PageSize { get; set; }             // Page size

        public List<T> Data { get; set; }


       
    }

    public class PagedRequest<T>
    {
        public int PageIndex { get; set; }            // Current page
        public int PageSize { get; set; }             // Page size
        public T Filter { get; set; }
    }
}
