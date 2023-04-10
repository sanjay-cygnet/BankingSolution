namespace BuildingBlocks.Shared.Extensions;
public static class StringExtensions
{
    public static bool IsNull(this string? val)
    {
        return string.IsNullOrEmpty(val) ? true : false;
    }
}
