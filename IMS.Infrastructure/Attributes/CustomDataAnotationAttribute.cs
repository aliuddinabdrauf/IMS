using System.Collections;
using System.ComponentModel.DataAnnotations;
using IMS.Infrastructure.Util;

namespace IMS.Infrastructure.Attributes;

public class StringArrayLengthAttribute : ValidationAttribute
{
    public int MinimumLength { get; init; } = 0;
    public int MaximumLength { get; init; } = Int32.MaxValue;

    public override bool IsValid(object? value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value is IEnumerable arrayOfString) 
            return arrayOfString.Cast<string>().ToList().TrueForAll(t => t.Length >= MinimumLength && t.Length <= MaximumLength);
        return true;
    }
}

public class ArrayOnlyHaveEmailAddressAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        ArgumentNullException.ThrowIfNull(value);
        var validator = new EmailAddressAttribute();
        if (value is IEnumerable arrayOfString)
        {
            return arrayOfString.Cast<string>().ToList().TrueForAll(e => validator.IsValid(e));
        }
        return true;
    }
}

public class PasswordIsValidAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return value.ToString().ValidAsPassword();
    }
}