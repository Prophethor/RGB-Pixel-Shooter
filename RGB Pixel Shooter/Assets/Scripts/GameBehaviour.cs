using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public enum RGBColor
{
    Red, Green, Blue
}

public class GameBehaviour : MonoBehaviour
{
    public static RGBColor RandColor()
    {
        var values = System.Enum.GetValues(typeof(RGBColor));
        return (RGBColor)values.GetValue(Random.Range(0,values.Length));
    }
}
