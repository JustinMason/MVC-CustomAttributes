using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MVC.Validation.DateComparer
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DateComparerAttribute : ValidationAttribute, IClientValidatable
    {
        private bool _minAddSet = false;
        private bool _maxAddSet = false;
        private double _minDateAddDaysFromNow;
        private double _maxDateAddDaysFromNow;
        private ModelMetadata _metadata;

        /// <summary>
        /// If set will use DateTime.AddDays to specify the Min Valid Date
        /// </summary>
        public double MinDateAddDaysFromNow
        {
            get { return _minDateAddDaysFromNow; }
            set
            {
                _minAddSet = true;
                _minDateAddDaysFromNow = value;
            }
        }

        /// <summary>
        /// If set will use DateTime.AddDays to specify the Max Valid Date
        /// </summary>
        public double MaxDateAddDaysFromNow
        {
            get { return _maxDateAddDaysFromNow; }
            set
            {
                _maxAddSet = true;
                _maxDateAddDaysFromNow = value;
            }
        }

        /// <summary>
        /// The Proprty that JQuery and Reflection will use to get the value from the view model or page element
        /// </summary>
        public string MinDateSelector { get; set; }
        /// <summary>
        /// The Proprty that JQuery and Reflection will use to get the value from the view model or page element
        /// </summary>
        public string MaxDateSelector { get; set; }

        public override string FormatErrorMessage(string name)
        {
            if (String.IsNullOrEmpty(ErrorMessage))
            {
                return "Date is invalid";
            }
            else
            {
                return ErrorMessage;
            }
        }

        public DateTime? MinDate
        {
            get
            {
                if (String.IsNullOrEmpty(MinDateSelector))
                {
                    return _minAddSet ? DateTime.Now.AddDays(MinDateAddDaysFromNow) : DateTime.Now.AddYears(-20);
                }
                return null;
            }
        }

        public DateTime? MaxDate
        {
            get
            {
                if (String.IsNullOrEmpty(MaxDateSelector))
                {
                    return _maxAddSet ? DateTime.Now.AddDays(MaxDateAddDaysFromNow) : DateTime.Now.AddYears(20);
                }
                return null;
            }
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata,
                                                                               ControllerContext context)
        {
            return new[]
                {
                    new ModelClientValidationRangeDateRule(FormatErrorMessage(metadata.GetDisplayName()), MinDateSelector, MaxDateSelector, MinDate, MaxDate, metadata.IsNullableValueType)
                };
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime? minDate;
            DateTime? maxDate;
            var dateValue = value as DateTime?;

            if (MinDate.HasValue)
            {
                minDate = MinDate.Value;
            }
            else
            {
                minDate = ReflectionHelper.GetPropValue(validationContext.ObjectInstance, MinDateSelector) as DateTime?;
            }

            if (MaxDate.HasValue)
            {
                maxDate = MaxDate.Value;
            }
            else
            {
                maxDate = ReflectionHelper.GetPropValue(validationContext.ObjectInstance, MaxDateSelector) as DateTime?;
            }

            if ((!dateValue.HasValue) ||
                (minDate <= dateValue && dateValue <= maxDate))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(string.Empty));
        }
    }
}