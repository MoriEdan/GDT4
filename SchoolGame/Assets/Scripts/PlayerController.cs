using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    private bool hasTurn;

	// Use this for initialization
	void Start ()
    {
        hasTurn = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(!hasTurn)
        {
            //skip update
        }
	}

    public void giveTurn()
    {
        //bla bla begin turn
        hasTurn = true;
    }

    public void endTurn()
    {
        //bla bla end turn
        hasTurn = false;
    }
}
