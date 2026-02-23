using Application.DTOs.ApiResponseDTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebAPI.DataHandler.Validation
{
    public static class ValidationHandler
    {
        /// <summary>
        /// Extracts validation errors from the Model State and formats them into a standard API error response.
        /// </summary>
        /// <param name="modelState">The dictionary containing the state of the model and any validation errors.</param>
        /// <returns>An <see cref="ApiResponse"/> object containing a list of validation failure details.</returns>
        public static ApiResponse Handle(ModelStateDictionary modelState)
        {
            var errors = modelState
                .SelectMany(x => x.Value!.Errors.Select(e => new
                {
                    Field = x.Key,
                    Message = e.ErrorMessage,
                }))
                .ToList();

            return ApiResponse.Fail("Validation failed", errors);
        }
    }
}