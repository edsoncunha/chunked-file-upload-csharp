using Microsoft.AspNetCore.Mvc;
using System;
using ChunkedUploadWebApi.Model;

namespace ChunkedUploadWebApi.Exception
{
    public class SampleExceptionMapper
    {
        public JsonResult Map(System.Exception exception)
        {
            JsonResult result = new JsonResult(null);

            if (exception is NotFoundException)
            {
                result.StatusCode = 404;
            }
            else if (exception is BadRequestException)
            {
                result.StatusCode = 400;
            }
            else if (exception is ApiException)
            {
                result.StatusCode = 403;
            }
            else
            {
                result.StatusCode = 500;
            }

            result.Value = new ApiResponse(ApiResponse.ERROR, exception.Message);

            return result;
        }
    }
}