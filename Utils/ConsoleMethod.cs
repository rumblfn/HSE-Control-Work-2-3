namespace Utils;

/// <summary>
/// Class for common console methods.
/// </summary>
public static class ConsoleMethod
{
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
    public static void NicePrint(string? message, ConsoleColor color = CustomColor.DefaultColor, string? end = null)
    {
        Console.ForegroundColor = color;
        Console.Write((message ?? "") + (end ?? Environment.NewLine));
        Console.ResetColor();
    }

    /// <summary>
    /// Removes unnecessary characters or adds empty ones
    /// if the length is less than necessary.
    /// </summary>
    /// <returns>Formatted string.</returns>
    private static string GetFormattedValue(string? value, int needLength)
    {
        value ??= "";
        int emptySpaces = needLength - value.Length;
        if (emptySpaces > 0)
        {
            value += new string(' ', emptySpaces);
        }

        return value;
    }

    /// <summary>
    /// Outputs fields to the console.
    /// Adapts for console width.
    /// </summary>
    public static void PrintRecordsAsTable(List<List<List<string?>>> recordsArray)
    {
        int windowWidth = Console.WindowWidth - 1;
        int defaultColumnWidth = windowWidth / recordsArray[0].Count - 3;
        int lastColumnWidth = windowWidth - (defaultColumnWidth + 3) * (recordsArray[0].Count - 1) - 3;

        string rowsSeparator = new('-', windowWidth);
        int rowsMaxCountInRecord = recordsArray[0].Select(field => field.Count).Max();
        
        NicePrint($"Record(s): {recordsArray.Count - 1}", CustomColor.Primary);
        NicePrint(rowsSeparator, CustomColor.Secondary);
        
        foreach (List<List<string?>> record in recordsArray)
        {
            for (int j = 0; j < rowsMaxCountInRecord; j++)
            {
                string value;
                
                for (int columnIndex = 0; columnIndex < record.Count - 1; columnIndex++)
                {
                    NicePrint("|", CustomColor.Secondary, " ");
                    value = GetFormattedValue(record[columnIndex][0], defaultColumnWidth);
                    
                    NicePrint(value[..Math.Min(defaultColumnWidth, value.Length)], CustomColor.Primary, " ");
                    if (value.Length > defaultColumnWidth)
                    {
                        record[columnIndex][0] = value[Math.Min(defaultColumnWidth, value.Length)..];
                    }
                    else
                    {
                        record[columnIndex][0] = "";
                    }
                }
                
                NicePrint("|", CustomColor.Secondary, " ");
                value = GetFormattedValue(record[^1][j], lastColumnWidth);
                NicePrint(value[..Math.Min(value.Length, lastColumnWidth)], CustomColor.Primary, " ");
                NicePrint("|", CustomColor.Secondary, Environment.NewLine);
            }
            NicePrint(rowsSeparator, CustomColor.Secondary);
        }
    }
}