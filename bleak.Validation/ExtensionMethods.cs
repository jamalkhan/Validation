using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace bleak.Validation
{
    public static class ExtensionMethods
    {
        public static void Validate(this object instantiatedObject)
        {
            var errors = new List<Error>();
            ValidateClass(instantiatedObject, errors);
            ValidateProperties(instantiatedObject, errors);
            if (errors.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var error in errors)
                {
                    sb.AppendLine(error.FriendlyMessage);
                }
                throw new ValidationException(sb.ToString().Trim());
            }
        }

        private static void ValidateProperties(object instantiatedObject, List<Error> errors)
        {
            var properties = instantiatedObject.GetType().GetProperties().ToList();
            foreach (var property in properties)
            {
                errors.AddRange(property.Validate(instantiatedObject));
            }
        }

        private static void ValidateClass(object instantiatedObject, List<Error> errors)
        {
            var exceptions = new List<Exception>();
            foreach (var attribute in instantiatedObject.GetType().GetCustomAttributes(true))
            {
                if (attribute is ValidationAttribute)
                {
                    var validationAttribute = attribute as ValidationAttribute;
                    try
                    {
                        validationAttribute.Validate(instantiatedObject, instantiatedObject.GetType().Name);
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex);
                    }

                }
            }
            errors.AddRange(exceptions.ToErrors());
        }

        public static List<Error> Validate(this PropertyInfo property, object instantiatedObject)
        {
            // foreach ValidationAttribute on property
            //      check instantiatedObject.property for validity
            var exceptions = new List<Exception>();
            var propertyName = property.Name;
            foreach (var attribute in property.GetCustomAttributes(true))
            {
                if (attribute is ValidationAttribute)
                {
                    var validationAttribute = attribute as ValidationAttribute;
                    var type = property.PropertyType;
                    object value = property.GetValue(instantiatedObject, null);
                    try
                    {
                        validationAttribute.Validate(value, propertyName);
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex);
                    }
                }
            }
            return exceptions.ToErrors();
        }

        public static List<Error> ToErrors(this IEnumerable<Exception> exceptions)
        {
            var errors = new List<Error>();
            foreach (var exception in exceptions)
            {
                errors.Add(exception.ToError());
            }
            return errors;
        }

        public static Error ToError(this Exception exception)
        {
            var error = new Error();
            error.DebugDetails.Exception = exception;
            return error;
        }
        public static string ToDashedString(this IEnumerable<Error> errors)
        {
            StringBuilder builtString = new StringBuilder();
            foreach (Error error in errors)
            {

                builtString.Append("-  ");
                builtString.Append(error.FriendlyMessage);
                builtString.Append(System.Environment.NewLine);
            }
            return builtString.ToString();
        }
        public static string ToBulletedList(this IEnumerable<Error> errors)
        {
            StringBuilder builtString = new StringBuilder();
            builtString.Append("<ul>");
            foreach (Error error in errors)
            {

                builtString.Append("<li>");
                builtString.Append(error.FriendlyMessage);
                builtString.Append("</li>");
            }
            builtString.Append("</ul>");
            return builtString.ToString();
        }
    }

}
