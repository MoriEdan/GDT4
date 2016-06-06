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
        unitCap = GameSettings.Values.startUnitCap;
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

    public void refundUnitCostOnDeath(int unitCountToRefund)
    {
        unitCount -= unitCountToRefund;
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

    public void processIncome(int numOfFarms, int numOfMines, List<GameObject> Units)
    {
        //always get 10 food to keep the game going
        foodCount += 10 + (GameSettings.Values.FoodPerFarm * numOfFarms);
        goldCount += (GameSettings.Values.GoldPerMine * numOfMines);

        recalculateUnitCountAndCap(Units);
        
    }

    public void recalculateUnitCountAndCap(List<GameObject> Units)
    {
        unitCount = 0;
        unitCap = GameSettings.Values.startUnitCap;
        foreach (GameObject unit in Units)
        {
            switch(unit.name)
            {
                case "Soldier(Clone)":
                    unitCount += GameSettings.Values.soldierUnitCost;
                    break;
                case "Farm(Clone)":
                    unitCount += GameSettings.Values.farmUnitCost;
                    break;
                case "Mine(Clone)":
                    unitCount += GameSettings.Values.mineUnitCost;
                    break;
                case "House(Clone)":
                    unitCap += GameSettings.Values.UnitsPerHouse;
                    break;
            }
        }
    }

    public void updateResourcesOnGUI()
    {
        GUIManager.instance.updateResourcesGUI(foodCount, goldCount, unitCount, unitCap);
    }
}
