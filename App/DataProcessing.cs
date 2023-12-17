using Entities;
using Enums;

namespace App;

/// <summary>
/// Contains methods to work with data.
/// </summary>
public static class DataProcessing
{
    /// <summary>
    /// Retrieves the field value for
    /// the specified column from the table.
    /// </summary>
    /// <param name="theatre">Instance of the <see cref="Theatre"/>.</param>
    /// <param name="column">Column for the search.</param>
    /// <returns>Value in specified record and column.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Invalid column.</exception>
    private static string? GetValue(Theatre theatre, FilterColumns column)
    {
        return column switch
        {
            FilterColumns.ChiefName => theatre.ChiefName,
            FilterColumns.AdmArea => theatre.Contacts?.AdmArea,
            _ => throw new ArgumentOutOfRangeException(nameof(column))
        };
    }
    
    /// <summary>
    /// Selection of records for the specified column and search query.
    /// </summary>
    /// <param name="records">Array of entities Theatre.</param>
    /// <param name="columnName">Column for search.</param>
    /// <param name="sub">Search query.</param>
    /// <returns>Suitable entries, including headers.</returns>
    public static List<Theatre> SamplingByColumn(List<Theatre> records, FilterColumns columnName, string sub)
    {
        Console.WriteLine(columnName);
        sub = sub.ToLower();
        var data = new List<Theatre> { records[0] };
        
        for (int i = 1; i < records.Count; i++)
        {
            string? value = GetValue(records[i], columnName);
            if (value != null && value.ToLower().Contains(sub))
            {
                data.Add(records[i]);
            }
        }

        return data;
    }

    /// <summary>
    /// Sorting record by the specified column and type.
    /// </summary>
    /// <param name="records"></param>
    /// <param name="type">Default sorting types.</param>
    /// <returns>Array of fields.</returns>
    public static List<Theatre> SortingByCapacity(List<Theatre> records, SortType type)
    {
        Theatre headers = records[0];
        records = records.GetRange(1, records.Count - 1);
        records = new List<Theatre>(records.OrderByDescending(t => t.Capacity));
        
        if (type == SortType.Ascending)
        {
            records.Reverse();
        }
        
        records.Insert(0, headers);
        return records;
    }

    public static List<List<List<string?>>> GetPreview(List<Theatre> records, Direction direction, int limit)
    {
        limit -= 1;
        Theatre header = records[0];
        records = direction == Direction.Top
            ? records.GetRange(1, Math.Min(records.Count - 1, limit))
            : records.GetRange(Math.Max(records.Count - limit, 1), limit);
        records.Insert(0, header);
        return records.Select(record => record.ToMatrixView()).ToList();
    }
}