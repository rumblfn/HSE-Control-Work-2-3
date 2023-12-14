using System.Text;
using Entities;
using Utils;

namespace App;

/// <summary>
/// Csv methods.
/// </summary>
public static class CsvProcessing
{
    // Resources.
    private static string _fPath = string.Empty;

    /// <summary>
    /// Read data from file to matrix view.
    /// </summary>
    /// <param name="csvFilePath">Path fo file with csv data.</param>
    /// <returns>List of fields in arrays.</returns>
    private static List<string[]> ReadToMatrix(string csvFilePath)
    {
        var parser = new CsvParser(csvFilePath);
        string[][] matrix = parser.Parse();

        var template = new CsvTemplate(matrix);
        template.ValidateTemplate();

        return matrix.ToList();
    }

    /// <summary>
    /// Writes a csv file to a jagged array by filepath.
    /// </summary>
    /// <param name="csvFilePath">Path to file.</param>
    /// <returns>Csv data in array.</returns>
    public static List<Theatre> Read(string csvFilePath)
    {
        _fPath = csvFilePath;

        List<string[]> matrix = ReadToMatrix(csvFilePath);
        return MatrixToRecords(matrix);
    }

    /// <summary>
    /// Finds index for each column in headers.
    /// </summary>
    /// <param name="headers"></param>
    /// <returns>ColumnName to index in headers.</returns>
    private static Dictionary<string, int> HeaderToIndex(string[] headers)
    {
        var headerToIndex = new Dictionary<string, int>();
        foreach (string header in Utils.Constants.Headers.Keys)
        {
            headerToIndex[header] = Array.IndexOf(headers, header);
        }

        return headerToIndex;
    }

    /// <summary>
    /// Converts matrix to list of records. <see cref="Theatre"/>
    /// </summary>
    /// <param name="matrix">Array of parsed csv lines.</param>
    /// <returns>Parsed matrix.</returns>
    private static List<Theatre> MatrixToRecords(List<string[]> matrix)
    {
        Dictionary<string, int> headerToIndex = HeaderToIndex(matrix[0]);

        return matrix.Select(record => new Theatre
            {
                X_WGS = record[headerToIndex["X_WGS"]],
                Y_WGS = record[headerToIndex["Y_WGS"]],
                Contacts = new Contacts
                {
                    Fax = record[headerToIndex["Fax"]],
                    Email = record[headerToIndex["Email"]],
                    AdmArea = record[headerToIndex["AdmArea"]],
                    Address = record[headerToIndex["Address"]],
                    District = record[headerToIndex["District"]],
                    PublicPhone = record[headerToIndex["PublicPhone"]],
                },
                INN = record[headerToIndex["INN"]],
                OKPO = record[headerToIndex["OKPO"]],
                WebSite = record[headerToIndex["WebSite"]],
                FullName = record[headerToIndex["FullName"]],
                ShortName = record[headerToIndex["ShortName"]],
                ChiefName = record[headerToIndex["ChiefName"]],
                CommonName = record[headerToIndex["CommonName"]],
                WorkingHours = record[headerToIndex["WorkingHours"]],
                ChiefPosition = record[headerToIndex["ChiefPosition"]],
                ClarificationOfWorkingHours = record[headerToIndex["ClarificationOfWorkingHours"]],
                ROWNUM = record[headerToIndex["ROWNUM"]],
                GLOBALID = record[headerToIndex["GLOBALID"]],
                MainHallCapacity = record[headerToIndex["MainHallCapacity"]],
                AdditionalHallCapacity = record[headerToIndex["AdditionalHallCapacity"]],
                Capacity = NumberMethod.ParseInt(record[headerToIndex["MainHallCapacity"]]) + NumberMethod.ParseInt(record[headerToIndex["AdditionalHallCapacity"]]),
            })
            .ToList();
    }

    /// <summary>
    /// Converts array of fields to sb with separator and quotes.
    /// </summary>
    /// <param name="record">One line with fields in array.</param>
    /// <returns>Formatted data to write in file.</returns>
    private static StringBuilder RecordToSb(string?[] record)
    {
        var sb = new StringBuilder();
        foreach (string? field in record)
        {
            sb.Append(CsvParser.Quote + (field ?? string.Empty) + CsvParser.Quote + CsvParser.Separator);
        }

        sb.Remove(sb.Length - 1, 1);
        sb.Append(Environment.NewLine);
        
        return sb;
    }

    /// <summary>
    /// Overwrites file with specified data by saved path <see cref="_fPath"/>.
    /// </summary>
    /// <param name="data">Theatre entities to write.</param>
    /// <param name="nPath"></param>
    /// <param name="message"></param>
    public static string Write(List<Theatre> data, string nPath, string message = "")
    {
        try
        {
            var resultSb = new StringBuilder();
            foreach (Theatre theatre in data)
            {
                string?[] arrayOfFields = theatre.ToArray(Utils.Constants.Headers);
                resultSb.Append(RecordToSb(arrayOfFields));
            }
            
            File.WriteAllText(_fPath, resultSb.ToString());
            return message + Constants.DataSavedMessage;
        }
        catch (Exception ex)
        {
            return message + ex.Message;
        }
    }
    
    /// <summary>
    /// Appends text to file or creates file with specified text.
    /// </summary>
    /// <param name="data">Theatre entities to write.</param>
    /// <param name="nPath">Path to write.</param>
    public static string Add(List<Theatre> data, string nPath)
    {
        try
        {
            var resultSb = new StringBuilder();
            List<string[]> matrixOfExistingData = ReadToMatrix(nPath);
            Dictionary<string, int> headerToIndex = HeaderToIndex(matrixOfExistingData[0]);
            
            for (int i = 1; i < data.Count; i++)
            {
                Theatre theatre = data[i];
                string?[] arrayOfFields = theatre.ToArray(headerToIndex);
                resultSb.Append(RecordToSb(arrayOfFields));
            }
            
            File.AppendAllText(nPath, resultSb.ToString());
            return Constants.DataAddedMessage;
        }
        catch (Exception ex)
        {
            string message = $"Error in existing data: {ex.Message}" + Environment.NewLine
                             + "Trying to overwrite it." + Environment.NewLine;
            return Write(data, nPath, message);
        }
    }
}