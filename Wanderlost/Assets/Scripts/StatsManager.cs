using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour{
    public static string pace = "steady";
    public static string rations = "filling";

    public static void changePace(string newPace)
    {
        pace = newPace;
    }
}