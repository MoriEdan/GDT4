using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {

    private int player;

    private int unitHealth;
    private int movementRange;
    private int attackRange;
    private int attackDamage;

    private bool selected;

	// Use this for initialization
	void Start ()
    {
        player = 0;
        unitHealth = 1;
        movementRange = 0;
        attackRange = 0;
        attackDamage = 0;
	}

    public void setupUnit(int health, int movement, int range, int damage, int p)
    {
        unitHealth = health;
        movementRange = movement;
        attackRange = range;
        attackDamage = damage;
        player = p;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (selected)
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_Color", Color.green);
        }
        else
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_Color", Color.white);
        }
	}

    public void selectUnit()
    {
        selected = true;
    }

    public void deselectUnit()
    {
        selected = false;
    }

    public bool getSelected()
    {
        return selected;
    }

    public void assignUnitToPlayer(int p)
    {
        player = p;
    }

    public bool belongsToPlayer(int playerTurn)
    {
        if (playerTurn == player)
        {
            return true;
        }

        return false;
    }

    public int getUnitHealth()
    {
        return unitHealth;
    }

    public int getMovementRange()
    {
        return movementRange;
    }

    public int getAttackRange()
    {
        return attackRange;
    }

    public int getAttackDamage()
    {
        return attackDamage;
    }
}
