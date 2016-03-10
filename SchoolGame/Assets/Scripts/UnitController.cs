using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {

    private int player;

    private int health;
    private int movement;
    private int range;
    private int damage;

    private bool selected;

	// Use this for initialization
	void Start ()
    {
        player = 0;

        health = 100;
        movement = 0;
        range = 0;
        damage = 0;
	}

    public void setupUnit(int h, int m, int r, int d)
    {
        health = h;
        movement = m;
        range = r;
        damage = d;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (selected)
        {

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

    public int belongsToPlayer()
    {
        return player;
    }
}
