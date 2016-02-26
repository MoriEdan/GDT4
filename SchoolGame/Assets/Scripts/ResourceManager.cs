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

    public GameObject ResourceContainer;


	// Use this for initialization
	void Start ()
    {
        //init resources
        foodCount = 100;
        goldCount = 100;
        unitCount = 0;
        unitCap = 5;
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

    public void processIncome(int numOfFarms, int numOfMines)
    {
        foodCount += (20 * numOfFarms);
        goldCount += (50 * numOfMines);
    }

    public void updateResources()
    {
        Text[] Resources = ResourceContainer.GetComponentsInChildren<Text>();

        Resources[0].text = "Food: " + foodCount;
        Resources[1].text = "Gold: " + goldCount;
        Resources[2].text = "Units: " + unitCount + "/" + unitCap;
    }
}
