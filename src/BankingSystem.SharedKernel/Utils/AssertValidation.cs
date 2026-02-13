using System.Text.RegularExpressions;
using BankingSystem.SharedKernel.Domain.Exception;

namespace BankingSystem.SharedKernel.Utils;

public static class AssertValidation
{
    public static void EnsureExists(object? obj, string message)
    {
        if (obj is null)
            throw new DomainException(message);
    }

    public static void ValidateIfEqual(object obj1, object obj2, string message)
    {
        if (obj1.Equals(obj2))
            throw new DomainException(message);
    }

    public static void ValidateIfDifferent(object obj1, object obj2, string message)
    {
        if (!obj1.Equals(obj2))
            throw new DomainException(message);
    }

    public static void ValidateIfFalse(bool boolValue, string message)
    {
        if (!boolValue)
            throw new DomainException(message);
    }

    public static void ValidateIfTrue(bool boolValue, string message)
    {
        if (boolValue)
            throw new DomainException(message);
    }

    public static void ValidateIfDefault(DateTimeOffset dateTimeOffset, string message)
    {
        if (dateTimeOffset == default)
            throw new DomainException(message);
    }

    public static void ValidateIfNull(object? obj, string message)
    {
        if (obj is null)
            throw new DomainException(message);
    }

    public static void ValidateIfNullOrEmpty(string? text, string message)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new DomainException(message);
    }

    public static void ValidateLength(string text, int minLength, int maxLength, string message)
    {
        var length = text.Trim().Length;
        if (length < minLength || length > maxLength)
            throw new DomainException(message);
    }

    public static void ValidateIfLowerThen(long value, long min, string message)
    {
        if (value < min)
            throw new DomainException(message);
    }

    public static void ValidateIfGreaterThen(long value, long max, string message)
    {
        if (value > max)
            throw new DomainException(message);
    }

    public static void ValidateGuidIsEmpty(Guid guid, string message)
    {
        if (guid == Guid.Empty)
            throw new DomainException(message);
    }

    public static void ValidateIfContainsSpace(string text, string message)
    {
        if (text.Contains(" "))
            throw new DomainException(message);
    }

    public static void ValidateEmail(string email, string message)
    {
        var emailRegex = new Regex(
            @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-A-Za-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)+)(?<!\.)@[A-Za-z0-9][\w\.-]*[A-Za-z0-9]\.[A-Za-z][A-Za-z\.]*[A-Za-z]$",
            RegexOptions.IgnoreCase);

        if (!emailRegex.IsMatch(email))
            throw new DomainException(message);
    }
    
    public static void ValidateCollectionNotNullOrEmpty<T>(IEnumerable<T>? items, string message)
    {
        if (items is null || !items.Any())
            throw new DomainException(message);
    }

    public static void ValidateNoDuplicatesIgnoreCase(IEnumerable<string> items, string message)
    {
        if (items is null) throw new DomainException(message);

        var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var item in items)
        {
            var value = item;
            if (!set.Add(value))
                throw new DomainException(message);
        }
    }

    public static void ValidateAllEmails(IEnumerable<string> emails, Func<string, string> messageFactory)
    {
        if (emails is null)
            throw new DomainException("Email list cannot be null.");

        foreach (var email in emails)
            ValidateEmail(email, messageFactory(email));
    }
}