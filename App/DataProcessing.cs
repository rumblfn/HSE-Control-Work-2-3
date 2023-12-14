using Entities;

namespace App;

/// <summary>
/// Contains methods to work with data.
/// </summary>
public static class DataProcessing
{
    /// <summary>
    /// Selection of records for the specified column and search query.
    /// </summary>
    /// <param name="records">Array of entities Theatre.</param>
    /// <param name="columnName">Column for search.</param>
    /// <param name="sub">Search query.</param>
    /// <returns>Suitable entries, including headers.</returns>
    public static List<Theatre> SamplingByColumn(List<Theatre> records, string columnName, string sub)
    {
        Console.WriteLine(columnName);
        sub = sub.ToLower();
        var data = new List<Theatre> { records[0] };

        if (columnName == "ChiefName")
        {
            for (int i = 1; i < records.Count; i++)
            {
                string? chiefName = records[i].ChiefName;
                if (chiefName != null && chiefName.ToLower().Contains(sub))
                {
                    data.Add(records[i]);
                }
            }
        }
        else
        {
            for (int i = 1; i < records.Count; i++)
            {
                string? admArea = records[i].Contacts?.AdmArea;
                if (admArea != null && admArea.ToLower().Contains(sub))
                {
                    data.Add(records[i]);
                }
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
    public static List<Theatre> SortingByCapacity(List<Theatre> records, string type)
    {
        Theatre headers = records[0];
        records = records.GetRange(1, records.Count - 1);
        records = new List<Theatre>(records.OrderByDescending(t => t.Capacity));
        if (type == "Ascending")
        {
            records.Reverse();
        }
        
        records.Insert(0, headers);
        return records;
    }
}