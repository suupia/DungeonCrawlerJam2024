#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Save
{
    public static int GetFlamePoint()
    {
        return  PlayerPrefs.GetInt("FlamePoint", 0);
    }
    public static void SetFlamePoint(int value)
    {
        PlayerPrefs.SetInt("FlamePoint", value);
    }
}
