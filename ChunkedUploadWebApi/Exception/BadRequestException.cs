namespace ChunkedUploadWebApi.Exception
{
    public class BadRequestException : ApiException
    {
        private int code;

        public BadRequestException(string msg) : base(400, msg)
        {
            this.code = 400;
        }

        public BadRequestException(int code, string msg) : base(code, msg)
        {
            this.code = code;
        }
    }
}