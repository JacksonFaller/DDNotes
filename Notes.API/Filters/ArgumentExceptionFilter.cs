using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Notes.API.Filters
{
    public class ArgumentExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ArgumentException && 
                (context.Exception.Message.EndsWith("не найден")|| context.Exception.Message.EndsWith("не найдена")))
            {
                context.Response =
                    new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(context.Exception.Message)
                    };
                Logger.Logger.Instatnce.Error(
                    $"{context.Exception.StackTrace}{Environment.NewLine}{context.Exception.Message}");
            }
        }
    }
}