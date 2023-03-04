using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DocLibMan.Models
{
    public class AdminModel : IValidatableObject
    {
        public string Description { get; set; }

        public IEnumerable<IFormFile> Files { get; set; }

        public IEnumerable<string> FileInfo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // Validate the description
            if (!IsDescriptionValid(Description))
            {
                results.Add(new ValidationResult("The Description contains invalid characters!", new string[] { "description" }));
            }
            if(!(Files != null && Files.Any()))
            {
                results.Add(new ValidationResult("No File selected for upload!", new string[] { "files" }));
            }

            return results;
        }

        private bool IsDescriptionValid(string description)
        {
            if (String.IsNullOrEmpty(description)) return true;

            return new Regex(@"^[\x09\x0A\x0D\x20-\x7E\x80-\xFF]*$").IsMatch(description);
        }
    }
}
