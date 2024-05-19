using System.ComponentModel;

namespace Domain.Common;

public static class Validation
{

    public static bool IsValidEmail(this string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false; // suggested by @TK-421
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }

    public static Tuple<bool, string> IsValidString(this string? inp)
    {
        if (string.IsNullOrEmpty(inp))
            return Tuple.Create(false, "");
        else
            return Tuple.Create(true, inp);
    }

    public static Tuple<bool, T> IsNotNull<T>(this T? inp)
    {
        if (inp is null)
            return Tuple.Create(false, inp);
        else
            return Tuple.Create(true, inp);
    }
}
