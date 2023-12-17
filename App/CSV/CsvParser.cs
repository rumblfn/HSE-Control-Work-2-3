using System.Text;
namespace App.CSV;

/// <summary>
/// Contains methods for working with raw csv data.
/// And converts them to a normal form or vice versa.
/// </summary>
public class CsvParser
{
    public const char Separator = Utils.Constants.FieldsSeparator;
    public const char Quote = '"';

    private readonly string _fPath;
    private List<string> _pendingFieldLine;

    private bool _multiline;
    private string _pendingField = "";

    /// <summary>
    /// Initialization.
    /// </summary>
    /// <param name="path">Path to file.</param>
    public CsvParser(string path)
    {
        _fPath = path;
        _pendingFieldLine = new List<string>();
    }

    /// <summary>
    /// Parses strings using the rules of RFC 4180.
    /// </summary>
    /// <param name="line">One line of file.</param>
    /// <returns>Parsed fields.</returns>
    private IEnumerable<string> ParseLine(string line)
    {
        var result = new List<string>();
        bool withQuotes = false;
        bool quoted = false;

        StringBuilder field = new ();

        foreach (char ch in line)
        {
            if (ch == Quote && withQuotes)
            {
                if (field.Length > 0)
                {
                    field.Append(ch);
                    withQuotes = false;
                }
            }
            else
            {
                withQuotes = false;
            }

            if (_multiline)
            {
                field.Append(_pendingField + Environment.NewLine);
                _pendingField = "";
                quoted = true;
                _multiline = false;
            }

            if (ch == Quote)
            {
                quoted = !quoted;
            }
            else
            {
                if (ch == Separator && !quoted)
                {
                    result.Add(field.ToString());
                    field.Length = 0;
                }
                else
                {
                    field.Append(ch);
                }
            }
        }

        if (quoted)
        {
            _pendingField = field.ToString();
            _multiline = true;
        }
        else
        {
            result.Add(field.ToString());
        }

        return result;
    }
    
    /// <summary>
    /// Csv main parse function.
    /// </summary>
    /// <returns>Array of records.</returns>
    public List<List<string>> Parse()
    {
        string[] lines = File.ReadAllLines(_fPath, Encoding.UTF8);
        var matrix = new List<List<string>>();

        foreach (string line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            
            IEnumerable<string> csvLineInArray = ParseLine(line);
            if (_multiline)
            {
                _pendingFieldLine.AddRange(csvLineInArray);
            }
            else
            {
                var currentRecord = new List<string>();
                if (_pendingFieldLine is { Count: > 0 })
                {
                    currentRecord.AddRange(_pendingFieldLine);
                    currentRecord.AddRange(csvLineInArray);
                    _pendingFieldLine = new List<string>();
                }
                else
                {
                    currentRecord.AddRange(csvLineInArray);
                }

                matrix.Add(currentRecord);
            }
        }
        
        return matrix;
    }
}