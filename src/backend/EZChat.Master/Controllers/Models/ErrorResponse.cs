using System.Linq;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EZChat.Master.Controllers.Models
{
    public class ErrorResponse
    {
        public ErrorResponseCode Code { get; }
        public string Error { get; }

        public ErrorResponse(string error, ErrorResponseCode code = ErrorResponseCode.UnknownError)
        {
            Code = code;
            Error = error;
        }

        public ErrorResponse(IdentityResult identityResult)
        {
            Code = ErrorResponseCode.IdentityResultInvalid;
            Error = identityResult.Errors.FirstOrDefault()?.Description;
        }

        public ErrorResponse(ModelStateDictionary modelState)
        {
            Code = ErrorResponseCode.ModelStateInvalid;
            Error = modelState.Values.FirstOrDefault()?.Errors.FirstOrDefault()?.ErrorMessage;
        }
    }
}
