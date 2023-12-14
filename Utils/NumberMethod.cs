namespace Utils;

/// <summary>
/// Class for common number methods.
/// </summary>
public abstract class NumberMethod
{
    /// <summary>
    /// Parses string to integer number without errors.
    /// </summary>
    /// <param name="value">Value to parse.</param>
    /// <returns>Parsed value or zero.</returns>
    public static int ParseInt(string value)
    {
        return int.TryParse(value, out int x) ? x : 0;
    }
}