namespace Common;

public static class Validator
{
    public static void ThrowIfNull<T>(T? obj, string exceptionMessage)
    {
        if (obj == null)
        {
            throw new KeyNotFoundException(exceptionMessage);
        }
    }

    public static void ThrowForbidden()
    {
        throw new AccessViolationException("You do not have sufficient rights to perform this action");
    }
    
    public static void ThrowForbidden(string exceptionMessage)
    {
        throw new AccessViolationException(exceptionMessage);
    }
}