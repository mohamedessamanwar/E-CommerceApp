using BusinessAccessLayer.DTOS.Response;
using BusinessAccessLayer.Exception;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace BusinessAccessLayer.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (System.Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message };
                // Log.Error(error, error.Message, context.Request, "");

                //TODO:: cover all validation errors
                switch (error)
                {
                    case UnauthorizedAccessException e:
                        // custom application error
                        responseModel.Message = error.Message;
                        responseModel.StatusCode = HttpStatusCode.Unauthorized;
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;

                    //case ValidationException e:
                    //    // custom validation error
                    //    responseModel.Message = error.Message;
                    //    responseModel.StatusCode = HttpStatusCode.UnprocessableEntity;
                    //    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    //    break;
                    case KeyNotFoundException e:
                        // not found error
                        responseModel.Message = error.Message;
                        responseModel.StatusCode = HttpStatusCode.NotFound;
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case CustomValidationException e:
                        // can't update error
                        responseModel.Message = e.Message;
                        responseModel.StatusCode = HttpStatusCode.BadRequest;
                        responseModel.ErrorsDic = (Dictionary<string,List<string>>) e.Errors;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                  

                    default:
                        if (error.GetType().ToString() == "ApiException")
                        {
                            responseModel.Message += error.Message;
                            responseModel.Message += error.InnerException == null ? "" : "\n" + error.InnerException.Message;
                            responseModel.StatusCode = HttpStatusCode.BadRequest;
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                        }
                        else
                        {
                            responseModel.Message = error.Message;
                            responseModel.Message += error.InnerException == null ? "" : "\n" + error.InnerException.Message;

                            responseModel.StatusCode = HttpStatusCode.InternalServerError;
                            response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        }
                        break;

                }
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}
