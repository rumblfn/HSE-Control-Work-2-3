namespace Utils;

/// <summary>
/// The constant values used by both projects can be changed to any values.
/// </summary>
public struct Constants
{
    // Errors.
    public const string EmptyArrayMessage = "Empty array.";
    public const string DefaultErrorMessage = "Something went wrong.";
    public const string NotAbsolutePathErrorMessage =
        "The specified path does not contain a root, enter an absolute path.";
    public const string FileNotExistErrorMessage = "Nothing was found for the specified path.";

    public static readonly string FileRowsLengthErrorMessage = $"File contains less than {HeaderRowsCount} line(s). Where is headers?";
    public const string RowEndErrorMessage = "Row template is not correct.";
    public const string ColumnCountErrorMessage = "Error with count of columns.";
    public const string HeadersErrorMessage = "Error in headers.";
    
    // Default template headers.
    public static readonly Dictionary<string, int> Headers = new()
    {
        {"", 22},
        {"INN", 16},
        {"Fax", 10},
        {"OKPO", 15},
        {"X_WGS", 19},
        {"Y_WGS", 20},
        {"Email", 11},
        {"ROWNUM", 0 },
        {"WebSite", 14},
        {"AdmArea", 4},
        {"Address", 6},
        {"GLOBALID", 21},
        {"FullName", 2},
        {"District", 5},
        {"ShortName", 3},
        {"ChiefName", 7},
        {"CommonName", 1},
        {"PublicPhone", 9},
        {"WorkingHours", 12},
        {"ChiefPosition", 8},
        {"MainHallCapacity", 17},
        {"AdditionalHallCapacity", 18},
        {"ClarificationOfWorkingHours", 13},
    };
    public static readonly int ColumnCount = Headers.Keys.Count;
    public const int HeaderRowsCount = 1;
    public const char FieldsSeparator = ';';
}