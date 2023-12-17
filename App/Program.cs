﻿using System.Text;
using Entities;
using App.CSV;
using Utils;

namespace App;

/// <summary>
/// Main class of the file reader program.
/// </summary>
internal static class Program
{
    /// <summary>
    /// Checks for exit from the program.
    /// </summary>
    /// <returns>Key is not <see cref="Constants.ExitKeyboardKey"/>.</returns>
    private static bool HandleAgain()
    {
        ConsoleMethod.NicePrint(Constants.AgainMessage, CustomColor.SystemColor);
        return ConsoleMethod.ReadKey() != Constants.ExitKeyboardKey;
    }
    
    /// <summary>
    /// Processing csv file.
    /// </summary>
    private static void Run()
    {
        ConsoleMethod.NicePrint(Constants.FileNameInputMessage);
        string userPathInput = ConsoleMethod.ReadLine();

        List<Theatre> records = CsvProcessing.Read(userPathInput);
        DataPanel panel = new(records);
        panel.Run();
    }
    
    /// <summary>
    /// Entry point of the program.
    /// </summary>
    private static void Main()
    {
        ConsoleMethod.NicePrint(Constants.ProgramStartedMessage, CustomColor.SystemColor);
        
        do
        {
            try
            {
                Run();
            }
            catch (Exception ex)
            {
                Exceptions.HandleDefaultException(ex);
            }
        } while (HandleAgain());
        
        ConsoleMethod.NicePrint(Constants.ProgramFinishedMessage, CustomColor.ProgressColor);
    }
}