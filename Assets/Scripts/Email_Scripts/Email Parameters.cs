using UnityEngine;
using UnityEngine.UI;

public static class EmailParameters
{
    public static string HighlightedWordColor = "#4093ff";
    public static string DiscrepancyColor =  "#c22121";

    public static Color CorrectEmailNormalColor = GetColor("#C2FF99");
    public static Color IncorrectEmailNormalColor = GetColor("#FF8D8F");
    
    public static Color DefaultEmailNormalColor = GetColor("#FFFFFF");
    public static Color DefaultEmailHighlightedColor = GetColor("#EEEEEE");
    public static Color DefaultEmailPressedColor = GetColor("#D1D1D1");
    public static Color DefaultEmailSelectedColor = GetColor("#C8C8C8");
    
    public static Color GetColor(string hex)
    {
        ColorUtility.TryParseHtmlString(hex, out Color color);
        return color;
    }
}