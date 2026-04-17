using UnityEngine;
using UnityEngine.UI;

public static class EmailParameters
{
    public static string HighlightedWordColor = "#50cc3f";
    public static string DiscrepancyColor =  "#c22121";

    public static Color CorrectEmailNormalColor = GetColor("#cbdbbd");
    public static Color CorrectEmailHighlightedColor = GetColor("#b4c2a9");
    public static Color CorrectEmailPressedColor = GetColor("#7e916e");
    public static Color CorrectEmailSelectedColor = GetColor("#b4c2a9");
    
    public static Color IncorrectEmailNormalColor = GetColor("#d49b9c");
    public static Color IncorrectEmailHighlightedColor = GetColor("#c28d8e");
    public static Color IncorrectEmailPressedColor = GetColor("#a66f70");
    public static Color IncorrectEmailSelectedColor = GetColor("#c28d8e");
    
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