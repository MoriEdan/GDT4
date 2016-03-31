using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour
{ 
    private bool selected;
    private bool occupied;
    private bool canMoveTo;
    private bool canAttack;
    private bool[] usableByPlayer = new bool[] { false, false, false, false };

    // Use this for initialization
    void Start()
    {
        occupied = false;
        selected = false;
        canMoveTo = false;
        canAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_Color", Color.white);

        if (selected)
        {
            rend.material.SetColor("_Color", Color.cyan);
        }
        else if (canAttack)
        {
            rend.material.SetColor("_Color", Color.red);
        }
        else if (canMoveTo)
        {
            rend.material.SetColor("_Color", Color.gray);
        }
        else if (!selected && usableByPlayer[0] || usableByPlayer[1] || usableByPlayer[2] || usableByPlayer[3])
        {
            if (usableByPlayer[0] && MainGame.instance.getCurrentTurnForArray() == 0)
            {
                rend.material.SetColor("_Color", Color.green);
            }
            else if(usableByPlayer[1] && MainGame.instance.getCurrentTurnForArray() == 1)
            {
                rend.material.SetColor("_Color", Color.green);
            }
            else if(usableByPlayer[2] && MainGame.instance.getCurrentTurnForArray() == 2)
            {
                rend.material.SetColor("_Color", Color.green);
            }
            else if(usableByPlayer[3] && MainGame.instance.getCurrentTurnForArray() == 3)
            {
                rend.material.SetColor("_Color", Color.green);
            }
            else
            {
                rend.material.SetColor("_Color", Color.white);
            }            
        }
    }

    public bool getOccupied()
    {
        return occupied;
    }

    public void setOccupied(bool o)
    {
        occupied = o;
    }

    public bool getSelected()
    {
        return selected;
    }

    public void setSelected(bool select)
    {
        selected = select;
    }

    public bool getCanMoveTo()
    {
        if (canMoveTo && !occupied)
        {
            return true;
        }
        return false;
    }

    public void setMoveTo(bool move)
    {
        canMoveTo = move;
    }

    public bool getCanAttack()
    {
        return canAttack;
    }

    public void setCanAttack(bool attack)
    {
        canAttack = attack;
    }

    public bool getUsabiltyForPlayer(int player)
    {
        return usableByPlayer[player - 1];
    }

    public void setUsabilityForPlayer(int player, bool usable)
    {
        usableByPlayer[player - 1] = usable;
    }
}
