using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour{
    public static string pace = "Steady";
    public static string rations = "Filling";
    public static string weather = "Warm";
    public static string health = "Good";

    public static int day = 1;
    public static int seasonNum = 0;
    public static int food = 500;
    public static int nextWay = 100;
    public static string[] seasonsSet = { "Summer", "Autumn", "Winter", "Spring" };

    public static void changePace(string newPace)
    {
        pace = newPace;
    }

    public static void changeRations(string newRations)
    {
        rations = newRations;
    }

    public static void updateDate()
    {
        day += 1;
        if (day > 100)
        {
            day = 1;
            if (seasonNum == 3)
            {
                seasonNum = 0;
            }
            else
            {
                seasonNum++;
            }
        }
    }
}