using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static Wayfinder WF1 = new Wayfinder("The Tavern", 105, "The Tavern's Shop", "Foxspark");
    public static Wayfinder WF2 = new Wayfinder("1", 1, "1", "1");
    public static Wayfinder WF3 = new Wayfinder("1", 1, "1", "1");
    public static Wayfinder WF4 = new Wayfinder("1", 1, "1", "1");
    public static Wayfinder WF5 = new Wayfinder("1", 1, "1", "1");

    public static Wayfinder[] map = {
       WF1,
       WF2,
       WF3,
       WF4,
       WF5
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
    }

    /*
    Wayfinder createWF(string wName, int dist, string sName, string sKeeper)
    {
        Wayfinder temp = new Wayfinder(wName, dist, sName, sKeeper);

        return temp;
    }*/

}//Keerr, Keirr, Kierr
