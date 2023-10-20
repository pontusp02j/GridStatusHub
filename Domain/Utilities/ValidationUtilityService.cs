using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GridStatusHub.Domain.Requests.GridSystem;

namespace GridStatusHub.Domain.Utilities
{
    public class ValidationUtility
    {
        public List<ValidationResult> ValidateRequest(GridSystemRequest request)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(request);

            Validator.TryValidateObject(request, validationContext, validationResults, true);

            return validationResults;
        }
    }
}
