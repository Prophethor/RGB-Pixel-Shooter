using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities {
    
    public static string GetString (this RGBColor rgbColor) {
        switch(rgbColor) {
            case RGBColor.RED:
                return "Red";
            case RGBColor.GREEN:
                return "Green";
            case RGBColor.BLUE:
                return "Blue";
            default:
                return "White";
        }
    }

    public static Color GetColor (this RGBColor rgbColor) {
        switch (rgbColor) {
            case RGBColor.RED:
                return Color.red;
            case RGBColor.GREEN:
                return Color.green;
            case RGBColor.BLUE:
                return Color.blue;
            default:
                return Color.white;
        }
    }
}
