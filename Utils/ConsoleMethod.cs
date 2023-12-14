using Entities;
using Enums;

namespace Utils;

/// <summary>
/// Class for common console methods.
/// </summary>
public abstract class ConsoleMethod
{
    // Separators.
    private const string DefaultElementsSeparator = ", ";
    private static readonly string DefaultLinesSeparator = Environment.NewLine;

    /// <summary>
    /// Simple readKey method to avoid errors.
    /// </summary>
    /// <param name="intercept">Not display input?</param>
    /// <returns>Read key.</returns>
    public static ConsoleKey ReadKey(bool intercept = true)
    {
        try
        {
            return Console.ReadKey(intercept).Key;
        }
        catch
        {
            return ConsoleKey.Spacebar;
        }
    }

    /// <summary>
    /// Simple readLine method to avoid errors.
    /// </summary>
    /// <returns>Read or empty string.</returns>
    public static string ReadLine()
    {
        try
        {
            return Console.ReadLine() ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// Prints a message with a specified color and a specified line end.
    /// </summary>
    /// <param name="message">Message content.</param>
    /// <param name="color">Message color.</param>
    /// <param name="end">End of line.</param>
    public static void NicePrint(string message, ConsoleColor color = CustomColor.DefaultColor, string? end = null)
    {
        Console.ForegroundColor = color;
        Console.Write(message + (end ?? Environment.NewLine));
        Console.ResetColor();
    }

    /// <summary>
    /// Outputs an array to the console with the specified delimiters.
    /// </summary>
    /// <param name="arr">Common array.</param>
    /// <param name="elementsSeparator">Sep for elements.</param>
    /// <param name="linesSeparator">End for line.</param>
    public static void PrintArray(in string [][] arr, string? linesSeparator = null, string? elementsSeparator = DefaultElementsSeparator)
    {
        string[] arrayRows = new string[arr.Length];
        for (int i = 0; i < arr.Length; i++)
        {
            arrayRows[i] = string.Join(elementsSeparator, arr[i]);
        }

        string arrayString = string.Join(linesSeparator ?? DefaultLinesSeparator, arrayRows);

        NicePrint(
            arrayString.Length > 0 ? arrayString : Constants.EmptyArrayMessage, 
            CustomColor.ProgressColor);
    }

    /// <summary>
    /// Outputs fields to the console with the specified columns count.
    /// Where each field follows each other in array.
    /// Adjusts the length of the column relative to the data required for output
    /// and the width of the console.
    /// </summary>
    public static void PrintRecordsAsTable(List<Theatre> records, Direction direction, int limit)
    {
        NicePrint(records[0].ToString());
        limit -= 1;
        records = direction == 0
            ? records.GetRange(1, Math.Min(records.Count - 1, limit))
            : records.GetRange(Math.Max(records.Count - limit, 1), limit);
        
        foreach (Theatre record in records)
        {
            NicePrint(record.ToString());
        }
    }
}