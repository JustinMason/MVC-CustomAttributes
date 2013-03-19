using System;
using System.Web.Mvc;

namespace MVC.Validation.DateComparer
{
    public class ModelClientValidationRangeDateRule
        : ModelClientValidationRule
    {
        public ModelClientValidationRangeDateRule(string errorMessage,
                                                  string minDateSelector, string maxDateSelector, DateTime? minDate, DateTime? maxDate, bool IsNullableValueType)
        {
            ErrorMessage = errorMessage;
            ValidationType = "rangedate";

            ValidationParameters["minselector"] = minDateSelector;
            ValidationParameters["maxselector"] = maxDateSelector;
            ValidationParameters["mindate"] = minDate.HasValue ? minDate.Value.ToString("G") : "";
            ValidationParameters["maxdate"] = maxDate.HasValue ? maxDate.Value.ToString("G") : "";
            ValidationParameters["nullable"] = IsNullableValueType.ToString().ToLower();

        }
    }
}