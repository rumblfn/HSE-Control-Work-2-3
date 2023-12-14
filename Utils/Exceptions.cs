namespace Utils;

public static class Exceptions
{
    public static void HandleDefaultException(Exception ex)
    {
        ConsoleMethod.NicePrint(Constants.DefaultErrorMessage, CustomColor.ErrorColor);
        ConsoleMethod.NicePrint(ex.Message, CustomColor.ErrorColor);
    }
}