using App.Components;
using Entities;
using Enums;
using Utils;

namespace App;

/// <summary>
/// Panel menu (task manager) for working with data.
/// </summary>
internal class DataPanel
{
    private bool _toExit;
    private int _currentRowIndex = Console.CursorTop;
    private int _currentColumnIndex = Console.CursorLeft;

    private int _selectedRowIndex;
    private int _selectedColumnIndex;
    private MenuItem _selectedItem = new("Default", () => {});
    private MenuGroup _selectedGroup = new("Default", Array.Empty<MenuItem>());
    
    private List<Theatre> _data;
    private readonly MenuGroup[] _menuGroups;

    /// <summary>
    /// Initialization.
    /// </summary>
    /// <param name="records">Data of each theatre in single record.</param>
    public DataPanel(List<Theatre> records)
    {
        _data = records;
        _menuGroups = new[]
        {
            new MenuGroup("Filter by column", new MenuItem[]
            {
                new("ChiefName", () => HandleSelecting("ChiefName")),
                new("AdmArea", () => HandleSelecting("AdmArea")),
            }),
            new MenuGroup("Sort by Capacity", new MenuItem[]
            {
                new("Ascending", 
                    () => HandleSorting("Ascending")),
                new("Descending", 
                    () => HandleSorting("Descending")),
            }),
            new MenuGroup("Save", new MenuItem[]
            {
                new("Save to new file", () => HandleSave(SaveType.NewFile)),
                new("Add to exist file", () => HandleSave(SaveType.AddDataToExistFile)),
                new("Overwrite exist file", () => HandleSave(SaveType.OverwriteExistFile)),
            }),
            new MenuGroup("File", new MenuItem[]
            {
                new("Show", HandleShow),
                new("Exit", () => { _toExit = true; }),
            }),
        };
        UpdateSelectedItem();
    }

    /// <summary>
    /// It is used to prepare the canvas for work.
    /// </summary>
    /// <param name="message">Starting message.</param>
    private void Restore(string message)
    {
        Console.Clear();
        ConsoleMethod.NicePrint(message);
        UpdateCursorPosition();
    }

    /// <summary>
    /// Used to save the current data to a file.
    /// </summary>
    private void HandleSave(SaveType type)
    {
        ConsoleMethod.NicePrint("> Specify absolute path or default will be used:", CustomColor.Primary);
        string nPath = ConsoleMethod.ReadLine();
        string message = "";

        switch (type)
        {
            case SaveType.NewFile:
                if (File.Exists(nPath))
                {
                    ConsoleMethod.NicePrint("File exist it will be overwritten", CustomColor.WarningColor);
                }
                message = CsvProcessing.Write(_data, nPath);
                break;
            case SaveType.OverwriteExistFile:
                message = CsvProcessing.Write(_data, nPath);
                break;
            case SaveType.AddDataToExistFile:
                message = CsvProcessing.Add(_data, nPath);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        
        Restore(message);
    }
    
    /// <summary>
    /// Get show data direction top or bottom.
    /// Loop handler.
    /// </summary>
    /// <returns>Direction.</returns>
    private static Direction GetDirection()
    {
        while (true)
        {
            ConsoleMethod.NicePrint("> Specify direction top(t)/bottom(b)", CustomColor.Primary, ": ");
            string direction = ConsoleMethod.ReadLine().ToLower();

            switch (direction)
            {
                case "t":
                case "top":
                    return Direction.Top;
                case "b":
                case "bottom":
                    return Direction.Bottom;
                default:
                    ConsoleMethod.NicePrint("Specify correct direction.");
                    continue;
            }
        }
    }

    private int GetCount()
    {
        int limit = 0;
        do
        {
            ConsoleMethod.NicePrint(
                $"> Enter integer limit of records in range [2; {_data.Count}]", 
                CustomColor.Primary, ": ");
            
            string input = ConsoleMethod.ReadLine();
            limit = NumberMethod.ParseInt(input);
        } while (limit <= 1 || limit > _data.Count);

        return limit;
    }

    /// <summary>
    /// Displaying data in the console.
    /// </summary>
    private void HandleShow()
    {
        if (_data.Count > 1)
        {
            Direction direction = GetDirection();
            int count = GetCount();
        
            ConsoleMethod.PrintRecordsAsTable(_data, direction, count);
            ConsoleMethod.NicePrint("Press any key to continue.", CustomColor.ErrorColor);
            ConsoleMethod.ReadKey();
        
            Restore("Data showed.");
        }
        else
        {
            Restore("Empty collection of data.");
        }
    }

    /// <summary>
    /// Selecting data by columns and user search.
    /// </summary>
    /// <param name="column">Column to check.</param>
    private void HandleSelecting(string column)
    {
        ConsoleMethod.NicePrint(Constants.SearchMessage, CustomColor.Primary);
        string search = ConsoleMethod.ReadLine();
        _data = DataProcessing.SamplingByColumn(_data, column, search);
        Restore($"The new selection contains {_data.Count} record(s).");
    }

    /// <summary>
    /// Sorting data by specified column and type.
    /// </summary>
    /// <param name="sortType">Alphabetic or Descending.</param>
    private void HandleSorting(string sortType)
    {
        _data = DataProcessing.SortingByCapacity(_data, sortType);
        Restore($"{sortType} sorting by capacity completed.");
    }

    /// <summary>
    /// Updates the currently selected item.
    /// </summary>
    private void UpdateSelectedItem()
    {
        _selectedGroup.Selected = false;
        _selectedGroup = _menuGroups[_selectedRowIndex];
        _selectedGroup.Selected = true;

        _selectedItem.Selected = false;
        _selectedItem = _selectedGroup.Items[_selectedColumnIndex];
        _selectedItem.Selected = true;
    }

    /// <summary>
    /// Updates the indexes of the selected group and item.
    /// </summary>
    /// <param name="key">Pressed key.</param>
    private void HandleArrowKeys(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.DownArrow:
                if (_selectedRowIndex < _menuGroups.Length - 1)
                {
                    _selectedRowIndex++;
                    _selectedColumnIndex = 0;
                }
                break;
            case ConsoleKey.UpArrow:
                if (_selectedRowIndex > 0)
                {
                    _selectedRowIndex--;
                    _selectedColumnIndex = 0;
                }
                break;
            case ConsoleKey.LeftArrow:
                if (_selectedColumnIndex > 0)
                {
                    _selectedColumnIndex--;
                }
                break;
            case ConsoleKey.RightArrow:
                if (_selectedColumnIndex < _selectedGroup.Items.Length - 1)
                {
                    _selectedColumnIndex++;
                }
                break;
            default:
                return;
        }
        UpdateSelectedItem();
    }

    /// <summary>
    /// Panel shutdown.
    /// </summary>
    /// <param name="key">User pressed key.</param>
    private void HandleExitKey(ConsoleKey key)
    {
        if (key == ConsoleKey.Q)
        {
            _toExit = true;
        }
    }

    /// <summary>
    /// Processes the selected item.
    /// </summary>
    /// <param name="key">User pressed button key.</param>
    private void HandleEnterKey(ConsoleKey key)
    {
        if (key != ConsoleKey.Enter)
        {
            return;
        }

        _selectedItem.SelectAction.Invoke();
    }

    /// <summary>
    /// Updates cursor position of the console.
    /// </summary>
    private void UpdateCursorPosition()
    {
        _currentRowIndex = Console.CursorTop;
        _currentColumnIndex = Console.CursorLeft;
    }
    
    /// <summary>
    /// Panel runner.
    /// </summary>
    public void Run()
    {
        Restore(Constants.PanelMessage);
        
        while (!_toExit)
        {
            DrawPanel();
            ConsoleKey pressedButtonKey = ConsoleMethod.ReadKey();
            HandleArrowKeys(pressedButtonKey);
            HandleEnterKey(pressedButtonKey);
            HandleExitKey(pressedButtonKey);
        }
    }
    
    /// <summary>
    /// Displays the panel on the screen.
    /// </summary>
    private void DrawPanel()
    {
        Console.SetCursorPosition(_currentColumnIndex, _currentRowIndex);
        foreach (MenuGroup group in _menuGroups)
        {
            group.Write();
        }
    }
}