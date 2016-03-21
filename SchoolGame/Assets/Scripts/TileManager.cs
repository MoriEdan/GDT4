using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour {

    private bool selected;
    private bool occupied;
    private bool[] usableByPlayer = new bool[] { false, false, false, false };

    // Use this for initialization
    void Start()
    {
        occupied = false;
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_Color", Color.green);
        }
        else if (!selected && usableByPlayer[0] || usableByPlayer[1] || usableByPlayer[2] || usableByPlayer[3])
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");

            if (usableByPlayer[0])
            {
                rend.material.SetColor("_Color", Color.blue);
            }
            else if(usableByPlayer[1])
            {
                rend.material.SetColor("_Color", Color.yellow);
            }
            else if(usableByPlayer[2])
            {
                rend.material.SetColor("_Color", Color.magenta);
            }
            else if(usableByPlayer[3])
            {
                rend.material.SetColor("_Color", Color.red);
            }            
        }
        else
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_Color", Color.white);
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

    public void selectTile()
    {
        selected = true;
    }

    public void deselectTile()
    {
        selected = false;
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
