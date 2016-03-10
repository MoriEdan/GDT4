using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public Rigidbody Soldier;
    public Rigidbody Farm;

    private bool hasTurn;
    private ResourceManager resources;

    private int SoldierCount;
    private int farmCount;
    private int mineCount;
    private int houseCount;

    private List<GameObject> Units;

	// Use this for initialization
	void Start ()
    {
        hasTurn = false;
        resources = GetComponent<ResourceManager>();
        Units = new List<GameObject>();

        SoldierCount = 0;
        farmCount = 0;
        mineCount = 0;
        houseCount = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(hasTurn)
        {
            resources.updateResources();
        }
	}

    public bool buyUnit(string unit, GameObject selectedGameTile)
    {
        //Debug.Log("buying");
        if (unit == "Soldier")
        {
            //Debug.Log("Soldier");
            //Soldier cost: 50 food, 0 gold, 1 unit (1)
            if(resources.subtractUnitCost(50, 0, 1))
            {
                //Debug.Log("start placing");
                Units.Add((GameObject)Instantiate(Soldier, selectedGameTile.transform.position + new Vector3(0, 1f, 0), new Quaternion()));
                SoldierCount++;
                selectedGameTile.GetComponent<TileManager>().setOccupied(1);
                //Debug.Log("Done");
                return true;
            }
        }
        else if (unit == "Farm")
        {
            //Debug.Log("Farm");
            //Farm cost: 0 Food, 100 Gold, 1 Unit (2)
            if (resources.subtractUnitCost(0, 100, 1))
            {
                //Debug.Log("start placing")
                Units.Add((GameObject)Instantiate(Farm, selectedGameTile.transform.position + new Vector3(0, 1f, 0), new Quaternion()));
                farmCount++;
                selectedGameTile.GetComponent<TileManager>().setOccupied(2);
                //Debug.Log("Done");
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

    public void deselectAll()
    {
        foreach (GameObject o in Units)
        {
            o.GetComponent<UnitController>().deselectUnit();
        }
    }
}
