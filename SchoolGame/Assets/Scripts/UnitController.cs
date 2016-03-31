using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {

    private int player;

    private int unitHealth;
    private int movementRange;
    private int attackRange;
    private int attackDamage;

    private bool selected;

    private bool hasAttacked;
    private bool hasMoved;
    private bool canBeAttacked;

    // Use this for initialization
    void Awake ()
    {
        player = 0;
        unitHealth = 1;
        movementRange = 0;
        attackRange = 0;
        attackDamage = 0;

        hasAttacked = false;
        hasMoved = false;

        selected = false;

        Debug.Log("Start: " + ToString());
	}

    public void setupUnit(int health, int movement, int range, int damage, int player)
    {
        unitHealth = health;
        movementRange = movement;
        attackRange = range;
        attackDamage = damage;
        assignUnitToPlayer(player);

        //Debug.Log("setup: " + ToString());
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (selected)
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_Color", Color.blue);

            if (movementRange > 0 && !hasMoved)
            {
                GameField.instance.unSetAllMovable();

                Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, movementRange * 2);
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    if (hitColliders[i].tag == "GameTile")
                    {
                        hitColliders[i].GetComponent<TileManager>().setMoveTo(true);
                    }
                }
            }

            if (attackRange > 0 && !hasAttacked)
            {
                GameField.instance.unsetAllAttackable();

                Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, attackRange * 2);
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    if (hitColliders[i].tag == "GameTile")
                    {
                        hitColliders[i].GetComponent<TileManager>().setCanAttack(true);
                    }
                    else if(hitColliders[i].tag == "Unit")
                    {
                        if(!hitColliders[i].GetComponent<UnitController>().belongsToPlayer(player))
                        {
                            hitColliders[i].GetComponent<UnitController>().setCanBeAttacked(true);
                        }
                    }
                }
            }
        }
        else if(canBeAttacked)
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_Color", Color.red);
        }
        else
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_Color", Color.yellow);
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

    public void setHasAttacked(bool attack)
    {
        hasAttacked = attack;
    }

    public bool getHasAttacked()
    {
        return hasAttacked;
    }

    public void setHasMoved(bool move)
    {
        hasMoved = move;
    }

    public bool getHasMoved()
    {
        return hasMoved;
    }

    public bool getCanBeAttacked()
    {
        return canBeAttacked;
    }

    public void setCanBeAttacked(bool attack)
    {
        canBeAttacked = attack;
    }

    public void attackUnit(int damage)
    {
        unitHealth -= damage;

        if(unitHealth <= 0)
        {
            GetComponentInParent<PlayerController>().removeUnit(this.GetComponentInParent<Rigidbody>());
        }
    }

    public void moveUnit(Rigidbody unit, GameObject tile)
    {
        if (tile != null)
        {
            TileManager newTile = tile.GetComponent<TileManager>();

            if (newTile.getCanMoveTo() && !newTile.getOccupied())
            {
                GameField.instance.resetOccupiedUnderUnit(unit);

                newTile.setOccupied(true);
                unit.position = tile.transform.position + new Vector3(0, 1f, 0);
                unit.GetComponent<UnitController>().setHasMoved(true);
            }
        }
    }

    public void attackWithUnit(Rigidbody unitToAttack)
    {
        unitToAttack.GetComponent<UnitController>().attackUnit(attackDamage);
    }

    void OnMouseEnter()
    {
        GUIManager.instance.currentToolTipText = ToString();
    }

    void OnMouseExit()
    {
        GUIManager.instance.currentToolTipText = "";
    }

    //for easy debugging
    public override string ToString()
    {
        string stringToReturn = "";
        int index;
        index = this.name.IndexOf("(");

        if (index > 0)
        {
            stringToReturn = stringToReturn + this.name.Substring(0, index) + "\n";
        }
        else
        {
            stringToReturn = stringToReturn + this.name + "\n";
        }

        stringToReturn = stringToReturn + "Player: " + player + "\n";
        stringToReturn = stringToReturn + "Health: " + unitHealth + "\n";
        stringToReturn = stringToReturn + "Damage: " + attackDamage + "\n";
        stringToReturn = stringToReturn + "Attack Range: " + attackRange + "\n";
        stringToReturn = stringToReturn + "Movement Range: " + movementRange + "\n";
        return stringToReturn;
    }
}
