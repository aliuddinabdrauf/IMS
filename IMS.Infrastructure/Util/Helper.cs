using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using IMS.Infrastructure.DbContext.IMS;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace IMS.Infrastructure.Util;

public static class Helper
{
    private const string PasswordRegex = "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!#$&*@])[A-Za-z0-9!#$&*@]{8,20}$";

    public static bool ValidAsPassword(this string? password)
    {
        return password != null && Regex.IsMatch(password, PasswordRegex, RegexOptions.ECMAScript);
    }
    public static bool StringIsEmail(this string email)
    {
        var e = new EmailAddressAttribute();
        return e.IsValid(email);
    }

    public static bool IsCustomException(this Exception e)
    {
        return e.GetType().IsSubclassOf(typeof(CustomException));
    }
    public static bool IsSystemException(this Exception e)
    {
        return e.GetType().IsSubclassOf(typeof(SystemException));
    }
    public static void PrepareUpdate<TEntity>(this EntityEntry<TEntity> data, params string[] propToUpdate) where TEntity: TblBase
    {
        data.Property(o => o.Id).IsModified = false;
        data.Property(o => o.TimestampUpdated).IsModified = true;
        foreach (var prop in data.Properties.Where(o => propToUpdate.Contains(o.Metadata.Name)))
        {
            prop.IsModified = true;
        }
    }
}