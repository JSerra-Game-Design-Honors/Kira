using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class StatsManager : MonoBehaviour{
    public static GameObject one, two, three, four, five;

    public static GameObject[] party = new GameObject[5];
    public static bool[] activeParty = { true, true, true, true, true };

    public static string pace = "Steady";
    public static string rations = "Filling";
    public static string weather = "Warm";
    public static string health = "Good";
    public static int[] healthNum = { 100, 100, 100, 100, 100 };
    public static int averageHP= 100;

    public static int day = 1;
    public static int seasonNum = 0;
    public static int weatherNum = 0;
    public static int food = 5000;//****
    //public static int nextWay = 100;
    public static int alive = 5;

    public static string[] seasonsSet = { "Summer", "Autumn", "Winter", "Spring" };
    public static string[,] weatherSet = { { "Warm", "Hot", "Very Hot", "Heat Wave" }, {"Cool", "Cold", "Rainy", "Stormy"}, {"Cold", "Very Cold", "Snowy", "Blizzard"}, {"Cool", "Warm", "Rainy", "Stormy"} };

    static GameObject travelManager = GameObject.Find("TravelManager");


    public static void updateTravelerObjects(){
        one = GameObject.Find("Traveler1");
        two = GameObject.Find("Traveler2");
        three = GameObject.Find("Traveler3");
        four = GameObject.Find("Traveler4");
        five = GameObject.Find("Traveler5");

        Debug.Log("ID of one: "+one);

        party[0] = one;
        party[1] = two;
        party[2] = three;
        party[3] = four;
        party[4] = five;
    }

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

    public static int calculateGenPenalty()
    {
        int temp = 0;

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
        else if (weatherSet[seasonNum, weatherNum] == "Stormy")
        {
            temp += -3;
        }
        else if (weatherSet[seasonNum, weatherNum] == "Blizzard" || weatherSet[seasonNum, weatherNum] == "Heat Wave")
        {
            temp += -4;
        }

        //RATIONS FACTOR - applied after general penalty is returned (see update health)
        temp += calculateFoodPenalty();

        Debug.Log("penalty: " + temp);
        return temp;
    }

    public static int calculateFoodPenalty()
    {
        for(int i = 0; i < activeParty.Length; i++)
        {
            //Debug.Log("TRAVELER " + (i+1));
            if (activeParty[i])
            {
                if (rations == "Filling")
                {
                    if (food - 20 >= 0)
                    {
                        //FILL
                        food -= 20;
                        return 2;
                    }
                    else if (food - 10 >= 0)
                    {
                        //MEAGER
                        food -= 10;
                        return -4;
                    }
                    else if (food - 5 >= 0)
                    {
                        //BARE
                        food -= 5;
                        return -6;
                    }
                    else
                    {
                        //NO FOOD
                        Debug.Log("not enough food!");
                        return -10;
                    }
                }
                else if (rations == "Meager")
                {
                    if (food - 10 >= 0)
                    {
                        //MEAGER
                        food -= 10;
                        return -4;
                    }
                    else if (food - 5 >= 0)
                    {
                        //BARE
                        food -= 5;
                        return -6;
                    }
                    else
                    {
                        //NO FOOD
                        Debug.Log("not enough food!");
                        return -10;
                    }
                }
                else
                {
                    if (food - 5 >= 0)
                    {
                        //BARE
                        food -= 5;
                        return -6;
                    }
                    else
                    {
                        //NO FOOD
                        Debug.Log("not enough food!");
                        return -10;
                    }
                }
            }
        }
        return 0;
    }

    public static void playerDeath(int i)
    {
        Debug.Log("die!");

        activeParty[i] = false;
        travelManager.GetComponent<TravelEvent>().startUpdate("Traveler " + (i + 1) + " has been lost to the Darkness.");
        party[i].SetActive(false);
    }


    public static void activateTravelers()
    {
        //UnityEngine.Debug.Log("in activation...");
        for (int i = 0; i < activeParty.Length; i++)
        {
            //UnityEngine.Debug.Log(party[i] + ":" + activeParty[i] + "== false");
            if (activeParty[i] == false)
            {
                //UnityEngine.Debug.Log("deactviate!!");
                party[i].SetActive(false);
            }
        }
    }

    public static void gameOver()
    {
        SceneManager.LoadScene(4);
    }
}