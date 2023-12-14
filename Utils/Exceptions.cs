namespace Utils;

/// <summary>
/// Exceptions handler.
/// </summary>
public static class Exceptions
{
    /// <summary>
    /// Default error handler.
    /// </summary>
    /// <param name="ex">Called exception.</param>
    public static void HandleDefaultException(Exception ex)
    {
        ConsoleMethod.NicePrint(Constants.DefaultErrorMessage, CustomColor.ErrorColor);
        ConsoleMethod.NicePrint(ex.Message, CustomColor.ErrorColor);
    }
}