using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour{
    public static string pace = "Steady";
    public static string rations = "Filling";
    public static string weather = "Warm";
    public static string health = "Good";
    public static int[] healthNum = { 100, 100, 100, 100, 100 };

    public static int day = 1;
    public static int seasonNum = 0;
    public static int weatherNum = 0;
    public static int food = 500;
    public static int nextWay = 100;
    public static int alive = 5;

    public static string[] seasonsSet = { "Summer", "Autumn", "Winter", "Spring" };
    public static string[,] weatherSet = { { "Warm", "Hot", "Very Hot", "Heat Wave" }, {"Cool", "Cold", "Rainy", "Stormy"}, {"Cold", "Very Cold", "Snowy", "Blizzard"}, {"Cool", "Warm", "Rainy", "Stormy"} };

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
    public static void chooseWeather()
    {
        int num = Random.Range(1, 101);

        if(num <= 30)
        {
            weather = weatherSet[seasonNum, 0];
            weatherNum = 0;
        }else if(num <= 70)
        {
            weather = weatherSet[seasonNum, 1];
            weatherNum = 1;
        }
        else if(num <= 95)
        {
            weather = weatherSet[seasonNum, 2];
            weatherNum = 2;
        }
        else
        {
            weather = weatherSet[seasonNum, 3];
            weatherNum = 3;
        }
    }
    //rations 30, pace 30, weather 20, other 20
    public static void updateHealth()
    {
        for (int i = 0; i < healthNum.Length; i++)
        {
            if(!(healthNum[i] <= 0))
            {
                healthNum[i] -= calculatePenalty();

                if (healthNum[i] <=0)
                {
                    //player death
                }

                //continue
            }
            
        }
            
    }

    public static int calculatePenalty()
    {
        int temp = 0;

        //RATIONS FACTOR
        if (rations == "Filling")
        {
            temp += +2;
        }
        else if (rations == "Meager")
        {
            temp += -4;
        }
        else
        {
            temp += -6;
        }

        //PACE FACTOR
        if (pace == "Steady")
        {
            temp += -2;
        }
        else if (pace == "Strenuous")
        {
            temp += -4;
        }
        else
        {
            temp += -6;
        }

        //WEATHER FACTOR
        if (weatherSet[seasonNum, weatherNum] == "Cool" || weatherSet[seasonNum, weatherNum] == "Warm")
        {
            temp += 0;
        }
        else if (weatherSet[seasonNum, weatherNum] == "Cold" || weatherSet[seasonNum, weatherNum] == "Hot")
        {
            temp += -1;
        }
        else if (weatherSet[seasonNum, weatherNum] == "Very Cold" || weatherSet[seasonNum, weatherNum] == "Very Hot" || weatherSet[seasonNum, weatherNum] == "Rainy")
        {
            temp += -2;
        }
        else if (weatherSet[seasonNum, weatherNum] == "Blizzard" || weatherSet[seasonNum, weatherNum] == "Heat Wave")
        {
            temp += -4;
        }

        return temp;
    }

    public static void updateFood()
    {
        if(rations == "Filling")
        {
            food -= 20 * alive;
        } else if(rations == "Meager")
        {
            food -= 10 * alive;
        }
        else
        {
            food -= 5 * alive;
        }
    }

    public static void updateDistance()
    {
        if (pace == "Steady")
        {
            nextWay -= 10;
        }
        else if (pace == "Strenuous")
        {
            nextWay -= 30;
        }
        else
        {
            nextWay -= 50;
        }
    }
}

/*
 * Health lost = pace penalty + rations penalty + weather penalty (+ random event) penalty
 * 
 * PACE
 * Steady: -2 (50 lower cap)
 * Strenuous: -4
 * Grueling: -6
 * 
 * RATIONS
 * Filling:
 * Meager:
 * Bare Bones:
 * 
 * int temp = 0;

        //RATIONS FACTOR
        if (rations == "Filling")
        {
            temp += Random.Range(25, 31);
        }
        else if (rations == "Meager")
        {
            temp += Random.Range(10, 21);
        }
        else
        {
            temp += Random.Range(0, 11);
        }

        //PACE FACTOR
        if (pace == "Steady")
        {
            temp += Random.Range(25, 31);
        }
        else if (pace == "Strenuous")
        {
            temp += Random.Range(10, 21);
        }
        else
        {
            temp += Random.Range(0, 11);
        }

        //WEATHER FACTOR
        if (weatherNum == 0)
        {
            temp += Random.Range(15, 21);
        }
        else if (weatherNum == 1)
        {
            temp += Random.Range(10, 16);
        }
        else if (weatherNum == 2)
        {
            temp += Random.Range(5, 11);
        }
        else
        {
            temp += Random.Range(0, 6);
        }

        //OTHER FACTOR
        temp += Random.Range(0, 21);
        Debug.Log(temp);

        //DETERMINE HEALTH
        if(temp < 50)
        {
            health = "Poor";
        }else if(temp == 50)
        {
            health = "Fair";
        }
        else
        {
            health = "Good";
        }
        healthNum = temp;
 */