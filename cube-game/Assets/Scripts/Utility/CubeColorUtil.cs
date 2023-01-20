using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class CubeColorUtil
{
    public static Color[] colorList = { Color.red, Color.magenta, Color.yellow, Color.green };
    public static bool IsSameRGB(this Color a, Color b)
    {
        return a.r == b.r && a.g == b.g && a.b == b.b;
    }
}
