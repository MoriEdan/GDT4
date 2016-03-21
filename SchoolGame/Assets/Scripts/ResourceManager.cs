using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    private int foodCount;
    private int goldCount;
    private int unitCount;
    private int unitCap;

	// Use this for initialization
	void Start ()
    {
        //init resources
        foodCount = GameSettings.Values.startFood;
        goldCount = GameSettings.Values.startGold;
        unitCount = GameSettings.Values.startUnitCount;
        unitCap = GameSettings.Values.startUnitCount;
	}

    public int getFoodCount()
    {
        return foodCount;
    }

    public int getGoldCount()
    {
        return goldCount;
    }

    public int getUnitCount()
    {
        return unitCount;
    }

    public bool subtractUnitCost(int foodCost, int goldCost, int unitCost)
    {
        if (foodCost <= foodCount && goldCost <= goldCount && unitCount + unitCost <= unitCap)
        {
            foodCount -= foodCost;
            goldCount -= goldCost;
            unitCount += unitCost;

            return true;
        }
        return false;
    }

    public void processIncome(int numOfFarms, int numOfMines, int houseCount)
    {
        foodCount += (GameSettings.Values.FoodPerFarm * numOfFarms);
        goldCount += (GameSettings.Values.GoldPerMine * numOfMines);
        unitCap = GameSettings.Values.startUnitCap + GameSettings.Values.UnitsPerHouse * houseCount;
    }

    public void updateResourcesOnGUI()
    {
        GUIManager.instance.updateResourcesGUI(foodCount, goldCount, unitCount, unitCap);
    }
}
