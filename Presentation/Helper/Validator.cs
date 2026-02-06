using Application.Enums;
using FluentValidation;
using Presentation.ViewModels.Response;

namespace Presentation.Helper
{
    public class Validator<T> where T : IValidator
    {
        private readonly IValidator<T> _validator;
        public Validator(IValidator<T> validator)
        {
            _validator = validator;
        }
        public  EndpointResponse<bool> Validate(T request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return EndpointResponse<bool>.Fail(ErrorCode.ValidationError, string.Join(", ", errors));
            }
            return EndpointResponse<bool>.Success(true);
        }
}
