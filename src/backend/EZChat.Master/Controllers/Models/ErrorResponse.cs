using System.Linq;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EZChat.Master.Controllers.Models
{
    public class ErrorResponse
    {
        public string Error { get; }

        public ErrorResponse(string error)
        {
            Error = error;
        }

        public ErrorResponse(IdentityResult identityResult)
        {
            Error = identityResult.Errors.FirstOrDefault()?.Description;
        }

        public ErrorResponse(ModelStateDictionary modelState)
        {
            Error = modelState.Values.FirstOrDefault()?.Errors.FirstOrDefault()?.ErrorMessage;
        }
    }
}
