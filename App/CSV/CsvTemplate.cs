namespace App.CSV;

/// <summary>
/// Class for validation csv data by template.
/// </summary>
public class CsvTemplate
{
    // Each field in single string and the data must be template-based.
    private readonly List<List<string>> _csvData;
    
    /// <param name="csvData">Matrix of data to check.</param>
    public CsvTemplate(List<List<string>> csvData)
    {
        _csvData = csvData;
    }

    /// <summary>
    /// Row count greater than or equal to <see cref="Utils.Constants.HeaderRowsCount"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException">Returns if row count is less than need.</exception>
    private void ValidateRowCount()
    {
        if (_csvData.Count < Utils.Constants.HeaderRowsCount)
        {
            throw new ArgumentNullException(Utils.Constants.FileRowsLengthErrorMessage);
        }
    }

    /// <summary>
    /// Validate by template headers column count.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Returns if any row contains another column count.
    /// </exception>
    private void ValidateColumnCount()
    {
        if (_csvData.Any(record => record.Count != Utils.Constants.ColumnCount))
        {
            throw new ArgumentNullException(Utils.Constants.ColumnCountErrorMessage);
        }
    }

    /// <summary>
    /// Checks headers for compliance.
    /// Columns can go in different order,
    /// but Russian and English headings must match the template.
    /// </summary>
    /// <exception cref="ArgumentNullException">Error in headers.</exception>
    private void ValidateHeaders()
    {
        List<string> headers = _csvData[0];
        foreach (KeyValuePair<string, int> header in Utils.Constants.Headers)
        {
            int index = headers.IndexOf(header.Key);
            if (index < 0)
            {
                throw new ArgumentNullException(Utils.Constants.HeadersErrorMessage);
            }
        }
    }

    /// <summary>
    /// Validates according to the calling <b>methods</b>.
    /// </summary>
    public void ValidateTemplate()
    {
        // Order of calling methods is important.
        ValidateRowCount();
        ValidateColumnCount();
        ValidateHeaders();
    }
}