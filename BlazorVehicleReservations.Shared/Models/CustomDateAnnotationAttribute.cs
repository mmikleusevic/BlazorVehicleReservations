using System.ComponentModel.DataAnnotations;

namespace BlazorVehicleReservations.Shared.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomDateAnnotationAttribute : ValidationAttribute
    {
        private string DateToCompareToFieldName { get; set; }
        public CustomDateAnnotationAttribute(string dateToCompareToFieldName)
        {
            DateToCompareToFieldName = dateToCompareToFieldName;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime earlierDate = (DateTime)value;

            DateTime laterDate = (DateTime)validationContext.ObjectType.GetProperty(DateToCompareToFieldName).GetValue(validationContext.ObjectInstance, null);

            if (laterDate > earlierDate)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Reservation Until is set earlier than Reservation From");
            }
        }
    }
}
