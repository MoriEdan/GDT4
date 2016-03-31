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

    //farm values
    public int farmFoodCost;
    public int farmGoldCost;
    public int farmUnitCost;
    public int farmHealth;
    public int farmMovementRange;
    public int farmAttackRange;
    public int farmAttackDamage;

    //mine values
    public int mineFoodCost;
    public int mineGoldCost;
    public int mineUnitCost;
    public int mineHealth;
    public int mineMovementRange;
    public int mineAttackRange;
    public int mineAttackDamage;

    //house values
    public int houseFoodCost;
    public int houseGoldCost;
    public int houseUnitCost;
    public int houseHealth;
    public int houseMovementRange;
    public int houseAttackRange;
    public int houseAttackDamage;

    // Use this for initialization
    void Awake ()
    {
        Values = this;
	}
	
}
