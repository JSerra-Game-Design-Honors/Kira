using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    /*
     * MAP MANAGER: Manages locations and current location, as well as map progression
     * 
     * Map:
     * An array of location objects that the game iterates through as the player progresses through the map
     * 
     * Map = {biome-> location 1}, {biome-> location 2}, {biome-> location 3}, {biome-> location 4}, {biome-> END}
     * 
     * Location Class:
     * Contains: Location Name, Shop Object, Miles to Location, Biome object, Crossing/Special Event Object?, People/Encounters?, Ability to trade
     * 
     * Shop Class:
     * Contains: Name of Shop, Shopkeeper, items/prices
     * 
     * Biome Object: Decides the biome players will travel in when traveling location
     * Contains: Monsters, Objects, Biome Type, Luck??
     */

    public static Wayfinder ST = new Wayfinder("Sheol City", 0, "City Emporium", "NAME");
    public static Wayfinder WF1 = new Wayfinder("City Wall", 50, "NULL", "NULL");
    public static Wayfinder WF2 = new Wayfinder("Inn", 100, "Inn Shop", "NAME");
    public static Wayfinder WF3 = new Wayfinder("Gate", 50, "NULL", "NULL");
    public static Wayfinder WF4 = new Wayfinder("Meadow", 100, "NULL", "NULL");
    public static Wayfinder WF5 = new Wayfinder("Village", 200, "General Store", "NAME");
    public static Wayfinder WF6 = new Wayfinder("River", 100, "NULL", "NULL");
    public static Wayfinder WF7 = new Wayfinder("Cornerstone", 150, "NULL", "NULL");
    public static Wayfinder WF8 = new Wayfinder("Tavern", 250, "Foxspark's Stash", "Foxspark");
    public static Wayfinder WF9 = new Wayfinder("Marsh", 150, "NULL", "NULL");
    public static Wayfinder WF10 = new Wayfinder("Willow", 150, "NULL", "NULL");
    public static Wayfinder WF11 = new Wayfinder("Hollow", 200, "NULL", "NULL");
    public static Wayfinder WF12 = new Wayfinder("Camp", 350, "Trading Post", "NAME");
    public static Wayfinder WF13 = new Wayfinder("Dragon", 200, "NULL", "NULL");
    public static Wayfinder WF14 = new Wayfinder("Chasm", 200, "NULL", "NULL");
    public static Wayfinder WF15 = new Wayfinder("Outpost", 250, "Outpost", "Greywren");
    public static Wayfinder WF16 = new Wayfinder("Graveyard", 350, "NULL", "NULL");
    public static Wayfinder WF17 = new Wayfinder("Gate", 500, "NULL", "NULL");
    public static Wayfinder EN = new Wayfinder("The Peak", 200, "NULL", "NULL");


    public static Wayfinder[] map = {
       ST,
       WF1,
       WF2,
       WF3,
       WF4,
       WF5,
       WF6,
       WF7,
       WF8,
       WF9,
       WF10,
       WF11,
       WF12,
       WF13,
       WF14,
       WF15,
       WF16,
       WF17,
       EN
    };

    public static int currLoc = 0;
    public static int currDist = map[0].distance;

    public class Wayfinder {
        public string name;
        public int distance;
        public Biome biome;
        public Shop shop;

        public Wayfinder(string wName, int dist, string sName, string sKeeper)
        {
            name = wName;
            distance = dist;

            Biome nBiome = new Biome();
            Shop nShop = new Shop(sName, sKeeper);
        }
    }

    public class Biome {
        //add customs **
    }

    public class Shop {
        public string name;
        public string shopkeeper; //turn to object? **
        //items

        public Shop(string sName, string sKeeper)
        {
            name = sName;
            shopkeeper = sName;
        }
    }
    
  public static void updateDistance()
    {
        if (StatsManager.pace == "Steady")
        {
            currDist -= 10;
        }
        else if (StatsManager.pace == "Strenuous")
        {
            currDist -= 30;
        }
        else
        {
            currDist -= 50;
        }

        if(currDist <= 0)
        {
            currDist = 0;
            GameObject.Find("TravelManager").GetComponent<TravelEvent>().arrivedAtWF();
        }
    }

    public static void updateLocation()
    {
        currLoc++;
        
        if(currLoc != map.Length)
        {
            currDist = map[currLoc].distance;
        }
    }
    /*
    Wayfinder createWF(string wName, int dist, string sName, string sKeeper)
    {
        Wayfinder temp = new Wayfinder(wName, dist, sName, sKeeper);

        return temp;
    }*/

}//Keerr, Keirr, Kierr
