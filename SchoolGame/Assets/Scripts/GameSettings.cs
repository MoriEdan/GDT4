using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour {

    public static GameSettings Values;

    //general game settings
    [Range(2, 4)]
    public int numberOfPlayers;

    //resource values
    public int startFood;
    public int startGold;
    public int startUnitCount;
    public int startUnitCap;
    public int FoodPerFarm;
    public int GoldPerMine;
    public int UnitsPerHouse;
    
    //game grid values
    public int gridSizeX;
    public int gridSizeZ;
    public int playerAreaX;
    public int PlayerAreaZ;

    //soldier values
    public int soldierFoodCost;
    public int soldierGoldCost;
    public int soldierUnitCost;
    public int soldierHealth;
    public int soldierMovementRange;
    public int soldierAttackRange;
    public int soldierAttackDamage;

	// Use this for initialization
	void Start ()
    {
        Values = this;
	}
	
}
