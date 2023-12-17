using Enums;
using Microsoft.VisualBasic.CompilerServices;

namespace Entities;

/// <summary>
/// An entity for a single record Theatre.
/// </summary>
public class Theatre
{
    public string? INN { get; init; }
    public string? OKPO { get; init; }
    public string? ROWNUM { get; init; }
    public string? GLOBALID { get; init; }
    public string? MainHallCapacity { get; init; }
    public string? AdditionalHallCapacity { get; init; }

    public int Capacity { get; init; }
    public string? X_WGS { get; init; }
    public string? Y_WGS { get; init; }
    
    public string? WebSite { get; init; }
    public string? FullName { get; init; }
    public string? ShortName { get; init; }
    public string? ChiefName { get; init; }
    public string? CommonName { get; init; }
    public string? WorkingHours { get; init; }
    public string? ChiefPosition { get; init; }
    public string? ClarificationOfWorkingHours { get; init; }

    // Address.
    public Contacts? Contacts { get; init; }

    /// <summary>
    /// Converts record to array view.
    /// </summary>
    /// <param name="headerToIndex">The ratio of the column to the index in the view.</param>
    /// <returns>Array of values with correct indexes.</returns>
    public string?[] ToArray(Dictionary<string, int> headerToIndex)
    {
        string?[] record = new string[headerToIndex.Count];
        record[headerToIndex["INN"]] = INN;
        record[headerToIndex["Fax"]] = Contacts?.Fax;
        record[headerToIndex["OKPO"]] = OKPO;
        record[headerToIndex["X_WGS"]] = X_WGS;
        record[headerToIndex["Y_WGS"]] = Y_WGS;
        record[headerToIndex["ROWNUM"]] = ROWNUM;
        record[headerToIndex["WebSite"]] = WebSite;
        record[headerToIndex["GLOBALID"]] = GLOBALID;
        record[headerToIndex["FullName"]] = FullName;
        record[headerToIndex["ShortName"]] = ShortName;
        record[headerToIndex["ChiefName"]] = ChiefName;
        record[headerToIndex["CommonName"]] = CommonName;
        record[headerToIndex["Email"]] = Contacts?.Email;
        record[headerToIndex["WorkingHours"]] = WorkingHours;
        record[headerToIndex["AdmArea"]] = Contacts?.AdmArea;
        record[headerToIndex["Address"]] = Contacts?.Address;
        record[headerToIndex["ChiefPosition"]] = ChiefPosition;
        record[headerToIndex["District"]] = Contacts?.District;
        record[headerToIndex["MainHallCapacity"]] = MainHallCapacity;
        record[headerToIndex["PublicPhone"]] = Contacts?.PublicPhone;
        record[headerToIndex["AdditionalHallCapacity"]] = AdditionalHallCapacity;
        record[headerToIndex["ClarificationOfWorkingHours"]] = ClarificationOfWorkingHours;

        return record;
    }

    /// <summary>
    /// Transforms record to comfort matrix view
    /// </summary>
    /// <returns></returns>
    public List<List<string?>> ToMatrixView()
    {
        var matrix = new List<List<string?>>
        {
            new ()
            {
                ROWNUM
            },
            new ()
            {
                FullName
            },
            new ()
            {
                ChiefName
            },
            new ()
            {
                MainHallCapacity
            },
            new ()
            {
                AdditionalHallCapacity
            },
            new ()
            {
                "a) " + Contacts?.Fax,
                "b) " + Contacts?.Email,
                "c) " + Contacts?.AdmArea,
                "d) " + Contacts?.Address,
                "e) " + Contacts?.District,
                "f) " + Contacts?.PublicPhone,
            },
        };

        return matrix;
    }
}