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

    public string?[] ToArray(Dictionary<string, int> headerToIndex)
    {
        string?[] record = new string[headerToIndex.Count];
        record[headerToIndex["INN"]] = INN;
        record[headerToIndex["Fax"]] = Contacts?.Fax;
        record[headerToIndex["OKPO"]] = OKPO;
        record[headerToIndex["X_WGS"]] = X_WGS;
        record[headerToIndex["Y_WGS"]] = Y_WGS;
        record[headerToIndex["Email"]] = Contacts?.Email;
        record[headerToIndex["ROWNUM"]] = ROWNUM;
        record[headerToIndex["WebSite"]] = WebSite;
        record[headerToIndex["AdmArea"]] = Contacts?.AdmArea;
        record[headerToIndex["Address"]] = Contacts?.Address;
        record[headerToIndex["GLOBALID"]] = GLOBALID;
        record[headerToIndex["FullName"]] = FullName;
        record[headerToIndex["District"]] = Contacts?.District;
        record[headerToIndex["ShortName"]] = ShortName;
        record[headerToIndex["ChiefName"]] = ChiefName;
        record[headerToIndex["CommonName"]] = CommonName;
        record[headerToIndex["PublicPhone"]] = Contacts?.PublicPhone;
        record[headerToIndex["WorkingHours"]] = WorkingHours;
        record[headerToIndex["ChiefPosition"]] = ChiefPosition;
        record[headerToIndex["MainHallCapacity"]] = MainHallCapacity;
        record[headerToIndex["AdditionalHallCapacity"]] = AdditionalHallCapacity;
        record[headerToIndex["ClarificationOfWorkingHours"]] = ClarificationOfWorkingHours;

        return record;
    }

    public override string ToString()
    {
        return $"{ROWNUM} | {ChiefName} | {MainHallCapacity} | {AdditionalHallCapacity} | a) {Contacts?.Fax} b) {Contacts?.Email} c) {Contacts?.AdmArea} d) {Contacts?.Address} e) {Contacts?.District} f) {Contacts?.PublicPhone}";
    }
}