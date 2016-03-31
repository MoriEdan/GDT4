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
	void Awake ()
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

        Debug.Log("buying");
        if (unit == "Soldier")
        {
            Debug.Log("Soldier");
            if(resources.subtractUnitCost(GameSettings.Values.soldierFoodCost, GameSettings.Values.soldierGoldCost, GameSettings.Values.soldierUnitCost))
            {
                //Debug.Log("start placing");
                newUnit = (Rigidbody)Instantiate(Soldier, selectedGameTile.transform.position + new Vector3(0, 1f, 0), new Quaternion());
                selectedGameTile.GetComponent<TileManager>().setOccupied(true);

                //Soldier set up
                newUnit.GetComponent<UnitController>().setupUnit(GameSettings.Values.soldierHealth, GameSettings.Values.soldierMovementRange,
                    GameSettings.Values.soldierAttackRange, GameSettings.Values.soldierAttackDamage, playerTurn);

                processNewUnit(newUnit);
                return true;
            }
        }
        else if (unit == "Farm")
        {
            //Debug.Log("Farm");
            if (resources.subtractUnitCost(GameSettings.Values.farmFoodCost, GameSettings.Values.farmGoldCost, GameSettings.Values.farmUnitCost))
            {
                //Debug.Log("start placing")
                newUnit = (Rigidbody)Instantiate(Farm, selectedGameTile.transform.position + new Vector3(0, 1f, 0), new Quaternion());
                selectedGameTile.GetComponent<TileManager>().setOccupied(true);

                //Farm set up
                newUnit.GetComponent<UnitController>().setupUnit(GameSettings.Values.farmHealth, GameSettings.Values.farmMovementRange,
                    GameSettings.Values.farmAttackRange, GameSettings.Values.farmAttackDamage, playerTurn);

                processNewUnit(newUnit);
                return true;
            }
        }
        else if (unit == "Mine")
        {
            //Debug.Log("mine");
            if (resources.subtractUnitCost(GameSettings.Values.mineFoodCost, GameSettings.Values.mineGoldCost, GameSettings.Values.mineUnitCost))
            {
                //Debug.Log("start placing")
                newUnit = (Rigidbody)Instantiate(Mine, selectedGameTile.transform.position + new Vector3(0, 1f, 0), new Quaternion());
                selectedGameTile.GetComponent<TileManager>().setOccupied(true);

                //mine set up
                newUnit.GetComponent<UnitController>().setupUnit(GameSettings.Values.mineHealth, GameSettings.Values.mineMovementRange,
                    GameSettings.Values.mineAttackRange, GameSettings.Values.mineAttackDamage, playerTurn);

                processNewUnit(newUnit);
                return true;
            }
        }
        else if (unit == "House")
        {
            Debug.Log("house");
            if (resources.subtractUnitCost(GameSettings.Values.houseFoodCost, GameSettings.Values.houseGoldCost, GameSettings.Values.houseUnitCost))
            {
                Debug.Log("start placing");
                newUnit = (Rigidbody)Instantiate(House, selectedGameTile.transform.position + new Vector3(0, 1f, 0), new Quaternion());
                selectedGameTile.GetComponent<TileManager>().setOccupied(true);

                //house set up
                newUnit.GetComponent<UnitController>().setupUnit(GameSettings.Values.houseHealth, GameSettings.Values.houseMovementRange,
                    GameSettings.Values.houseAttackRange, GameSettings.Values.houseAttackDamage, playerTurn);

                processNewUnit(newUnit);
                return true;
            }
        }
        //add more units

        return false;
    }

    private void processNewUnit(Rigidbody newUnit)
    {
        newUnit.transform.SetParent(this.transform);
        Units.Add(newUnit);
        Debug.Log(newUnit.GetComponent<UnitController>().ToString());
    }

    public void giveTurn()
    {
        hasTurn = true;

        int farmCount = 0;
        int mineCount = 0;
        int houseCount = 0;

        foreach (Rigidbody unit in Units)
        {
            Debug.Log(unit);
            Debug.Log(Farm);

            if (unit.name == Farm.name + "(Clone)")
            {
                farmCount++;
            }
            else if (unit.name == Mine.name + "(Clone)")
            {
                mineCount++;
            }
            else if (unit.name == House.name + "(Clone)")
            {
                houseCount++;
            }
        }

        resources.processIncome(farmCount, mineCount, houseCount);
    }

    public void endTurn()
    {
        foreach (Rigidbody unit in Units)
        {
            unit.GetComponent<UnitController>().setHasMoved(false);
            unit.GetComponent<UnitController>().setHasAttacked(false);
        }
        hasTurn = false;
    }

    public void deselectAllUnits()
    {
        foreach (Rigidbody unit in Units)
        {
            unit.GetComponent<UnitController>().setCanBeAttacked(false);
            unit.GetComponent<UnitController>().deselectUnit();
        }
    }

    public UnitController getSelectedUnitController()
    {
        foreach (Rigidbody unit in Units)
        {
            if (unit.GetComponent<UnitController>().getSelected())
            {
                return unit.GetComponent<UnitController>();
            }
        }

        return null;
    }

    public Rigidbody getSelectedUnit()
    {
        foreach (Rigidbody unit in Units)
        {
            if (unit.GetComponent<UnitController>().getSelected())
            {
                return unit;
            }
        }

        return null;
    }

    public void removeUnit(Rigidbody UnitToRemove)
    {
        foreach (Rigidbody unit in Units)
        {
            if(unit == UnitToRemove)
            {
                GameField.instance.resetOccupiedUnderUnit(UnitToRemove);
                Units.Remove(UnitToRemove);
                Destroy(UnitToRemove.gameObject);
                return;
            }
        }
    }
}
