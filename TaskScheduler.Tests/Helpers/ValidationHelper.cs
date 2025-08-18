using System.ComponentModel.DataAnnotations;

namespace TaskScheduler.Tests.Helpers
{
    
    public static class ValidationHelper
    {
        public static IList<ValidationResult> Validate(object obj)
        {
            var context = new ValidationContext(obj, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(obj, context, results, validateAllProperties: true);
            
            return results;
        }
    }
}