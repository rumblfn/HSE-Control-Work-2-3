using System.Text;

namespace App;

/// <summary>
/// Contains methods for working with raw csv data.
/// And converts them to a normal form or vice versa.
/// </summary>
public class CsvParser
{
    public const char Separator = Utils.Constants.FieldsSeparator;
    public const char Quote = '"';

    private readonly string _fPath;
    private string[] _pendingFieldLine;

    private bool _multiline;
    private string _pendingField = "";

    /// <summary>
    /// Initialization.
    /// </summary>
    /// <param name="path">Path to file.</param>
    public CsvParser(string path)
    {
        _fPath = path;
        _pendingFieldLine = Array.Empty<string>();
    }

    /// <summary>
    /// Merges all arrays into the first one.
    /// </summary>
    /// <param name="baseArr">Target array.</param>
    /// <param name="arrays">Other arrays.</param>
    private static void ConcatArrays(ref string[] baseArr, params string[][] arrays)
    {
        baseArr = arrays
            .Aggregate(baseArr, (current, arr) => current.Concat(arr).ToArray());
    }

    /// <summary>
    /// Parses strings using the rules of RFC 4180.
    /// </summary>
    /// <param name="line">One line of file.</param>
    /// <returns>Parsed fields.</returns>
    private string[] ParseLine(string line)
    {
        string[] result = Array.Empty<string>();
        bool quoted = false;
        bool withQuotes = false;

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
                    ConcatArrays(ref result, new[] { field.ToString() });
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
            ConcatArrays(ref result, new[] { field.ToString() });
        }

        return result;
    }
    
    /// <summary>
    /// Csv main parse function.
    /// </summary>
    /// <returns>Array of records.</returns>
    public string[][] Parse()
    {
        string[] lines = File.ReadAllLines(_fPath);
        string[][] matrix = new string[lines.Length][];
        int currentLineIndex = 0;

        foreach (string line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            
            string[] csvLineInArray = ParseLine(line);
            if (_multiline)
            {
                ConcatArrays(ref _pendingFieldLine, csvLineInArray);
            }
            else
            {
                matrix[currentLineIndex] = Array.Empty<string>();
                if (_pendingFieldLine is { Length: > 0 })
                {
                    ConcatArrays(ref matrix[currentLineIndex], _pendingFieldLine, csvLineInArray);
                    _pendingFieldLine = Array.Empty<string>();
                }
                else
                {
                    matrix[currentLineIndex] = csvLineInArray;
                }

                currentLineIndex++;
            }
        }
            
        matrix = matrix[..currentLineIndex];
        return matrix;
    }
}