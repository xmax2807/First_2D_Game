using UnityEngine;

public class SaveSlotData
{
    private string Format =>PlayerPrefs.GetString("DateFormatSave");
    public string TimePlayed;
    public string LastSavedDate
    {
        get
        {
            return _lastSavedDate.ToString(Format);
        }
        set
        { 
            System.DateTime.TryParseExact(
                value, 
                Format, 
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out _lastSavedDate
            );
        }
    }
    private System.DateTime _lastSavedDate;
}