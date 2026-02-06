using Application.Enums;
using Microsoft.AspNetCore.Connections.Features;

namespace Presentation.ViewModels.Response
{
    public record EndpointResponse<T>(T Data,ErrorCode ErroCode,string Message = "Request was successful", bool IsSuccess = true )
    {
        public static EndpointResponse<T> Success(T Data , string Message = "Successful Response")
        {
            return new EndpointResponse<T>(Data,ErrorCode.NoError,Message, true);
        }
        public static EndpointResponse<T> Fail(ErrorCode ErroCode , string Message = "Error Happend")
        {
            return new EndpointResponse<T>(default, ErroCode, Message, false);
        }
    }
}
