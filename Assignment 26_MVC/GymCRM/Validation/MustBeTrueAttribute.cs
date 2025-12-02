using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GymCRM.Validation
{
    /// <summary>
    /// يقبل القيمة TRUE فقط.
    /// يعمل سيرفر سايد + يطلّع data-val للـjQuery unobtrusive.
    /// </summary>
    public sealed class MustBeTrueAttribute : ValidationAttribute, IClientModelValidator
    {
        public MustBeTrueAttribute()
        {
            ErrorMessage = "يجب الموافقة على الشروط.";
        }

        public override bool IsValid(object? value) => value is bool b && b;

        public void AddValidation(ClientModelValidationContext context)
        {
            Merge(context.Attributes, "data-val", "true");
            Merge(context.Attributes, "data-val-mustbetrue", ErrorMessage!);
        }

        private static bool Merge(IDictionary<string, string> attrs, string key, string val)
        {
            if (attrs.ContainsKey(key)) return false;
            attrs.Add(key, val);
            return true;
        }
    }
}
