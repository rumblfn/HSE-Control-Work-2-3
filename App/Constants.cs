namespace App;

/// <summary>
/// Constant values (messages).
/// </summary>
internal struct Constants
{
    public const ConsoleKey ExitKeyboardKey = ConsoleKey.Q;
    public const string ProgramFinishedMessage = "Program finished.";
    
    public const string DataAddedMessage = "Data added to specified file.";
    public const string FileNameInputMessage = "Enter the file name:";
    public const string DataSavedMessage = "Data saved to file.";

    public const string PanelMessage = 
        "Press Q to exit.\n" +
        "This is a panel for working with a csv file.\n" +
        "Select an action using the arrow keys to select and Enter to confirm.\n" +
        "The console must be fully open, otherwise the characters will fit on top of each other.";

    public const string SearchMessage = "> What are we going to look for?";
    
    public static readonly string AgainMessage = $"Press any key to restart or {ExitKeyboardKey} to exit.";
    public const string ProgramStartedMessage = "Program started.";
}