using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Rigidbody Soldier;
    public Rigidbody Farm;

    private bool hasTurn;
    private ResourceManager resources;

    private int SoldierCount;
    private int farmCount;
    private int mineCount;

	// Use this for initialization
	void Start ()
    {
        hasTurn = false;
        resources = GetComponent<ResourceManager>();

        SoldierCount = 0;
        farmCount = 0;
        mineCount = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(hasTurn)
        {
            resources.updateResources();
        }
	}

    public bool buyUnit(string unit)
    {
        if (unit == "Soldier")
        {
            //Soldier cost: 50 food, 0 gold, 1 unit
            if(resources.subtractUnitCost(50, 0, 1))
            {
                Instantiate(Soldier, new Vector3(0, 1, 0), new Quaternion());
                SoldierCount++;
                return true;
            }
        }
        else if (unit == "Farm")
        {
            //Farm cost: 0 Food, 100 Gold, 1 Unit
            if (resources.subtractUnitCost(0, 100, 1))
            {
                Instantiate(Farm, new Vector3(1, 1, 1), new Quaternion());
                farmCount++;
                return true;
            }
        }
        //add more units

        return false;
    }

    public void giveTurn()
    {
        //Debug.Log("New turn: " + resources.getFoodCount() + ", " + resources.getGoldCount() + ", " + resources.getUnitCount());
        hasTurn = true;
        resources.processIncome(farmCount, mineCount);
    }

    public void endTurn()
    {
        //bla bla end turn
        hasTurn = false;
    }
}
