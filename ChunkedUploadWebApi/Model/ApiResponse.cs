using System;

namespace ChunkedUploadWebApi.Model
{
    [Serializable]
    public class ApiResponse
    {
        public const int ERROR = 1;
        public const int WARNING = 2;
        public const int INFO = 3;
        public const int OK = 4;
        public const int TOO_BUSY = 5;

        public int Code {get;set;}
        public string Type {get;set;}
        public string Message {get;set;}
        public string SessionId {get;set;}

        public ApiResponse()
        {
        }

        public ApiResponse(int code, String message)
        {
            this.Code = code;
            switch (code)
            {
                case ERROR:
                    Type = "error";
                    break;
                case WARNING:
                    Type = "warning";
                    break;
                case INFO:
                    Type = "info";
                    break;
                case OK:
                    Type = "ok";
                    break;
                case TOO_BUSY:
                    Type = "too busy";
                    break;
                default:
                    Type = "unknown";
                    break;
            }
            this.Message = message;
        }

        public ApiResponse(int code, String message, String sessionId) : this(code, message)
        {
            SessionId = sessionId;
        }

    }
}