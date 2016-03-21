using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public Rigidbody Soldier;
    public Rigidbody Farm;
    public Rigidbody Mine;
    public Rigidbody House;

    private bool hasTurn;
    private ResourceManager resources;

    private List<Rigidbody> Units;

	// Use this for initialization
	void Start ()
    {
        hasTurn = false;
        resources = GetComponent<ResourceManager>();
        Units = new List<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(hasTurn)
        {
            resources.updateResourcesOnGUI();
        }
	}

    public bool buyUnit(string unit, GameObject selectedGameTile, int playerTurn)
    {
        Rigidbody newUnit;

        //Debug.Log("buying");
        if (unit == "Soldier")
        {
            //Debug.Log("Soldier");
            if(resources.subtractUnitCost(GameSettings.Values.soldierFoodCost, GameSettings.Values.soldierGoldCost, GameSettings.Values.soldierUnitCost))
            {
                //Debug.Log("start placing");
                newUnit = (Rigidbody)Instantiate(Soldier, selectedGameTile.transform.position + new Vector3(0, 1f, 0), new Quaternion());
                selectedGameTile.GetComponent<TileManager>().setOccupied(true);

                //Soldier set up: 100 health, 4 walk range, 1 attack range, 50 attack damage
                newUnit.GetComponent<UnitController>().setupUnit(GameSettings.Values.soldierHealth, GameSettings.Values.soldierMovementRange,
                    GameSettings.Values.soldierAttackRange, GameSettings.Values.soldierAttackDamage, playerTurn);

                Units.Add(newUnit);
                Debug.Log("Done");
                return true;
            }
        }
        else if (unit == "Farm")
        {
            //Debug.Log("Farm");
            if (resources.subtractUnitCost(0, 100, 2))
            {
                //Debug.Log("start placing")
                newUnit = (Rigidbody)Instantiate(Farm, selectedGameTile.transform.position + new Vector3(0, 1f, 0), new Quaternion());
                selectedGameTile.GetComponent<TileManager>().setOccupied(true);

                //Farm set up: 500 health, 0 walk range, 0 attack range, 0 attack damage
                newUnit.GetComponent<UnitController>().setupUnit(500, 0, 0, 0, playerTurn);

                Units.Add(newUnit);           
                //Debug.Log("Done");
                return true;
            }
        }
        //add more units

        return false;
    }

    public void giveTurn()
    {
        hasTurn = true;

        int farmCount = 0;
        int mineCount = 0;
        int houseCount = 0;

        foreach (Rigidbody unit in Units)
        {
            if (unit == Farm)
            {
                farmCount++;
            }
            else if (unit == Mine)
            {
                mineCount++;
            }
            else if (unit == House)
            {
                houseCount++;
            }
        }

        resources.processIncome(farmCount, mineCount, houseCount);
    }

    public void endTurn()
    {
        //bla bla end turn
        hasTurn = false;
    }

    public void deselectAll()
    {
        foreach (Rigidbody r in Units)
        {
            r.GetComponent<UnitController>().deselectUnit();
        }
    }
}
