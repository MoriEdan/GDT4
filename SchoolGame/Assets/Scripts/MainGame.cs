using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MainGame : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;

    public Text PlayerTurn;

    private List<GameObject> Players;
    private int maxNumPlayers;
    private int playerTurn;

	// Use this for initialization
	void Start ()
    {
        //Add players to the list of available players
        Players = new List<GameObject>();
        Players.Add(Player1);
        Players.Add(Player2);

        //Make a random player start the game
        maxNumPlayers = 2;
        playerTurn = Random.Range(1, maxNumPlayers);
        getPlayerControlerCurrentTurn().giveTurn();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void nextTurn()
    {
        getPlayerControlerCurrentTurn().endTurn();
        playerTurn++;
        if(playerTurn > maxNumPlayers)
        {
            playerTurn = 1;
        }
        getPlayerControlerCurrentTurn().giveTurn();
    }

    private PlayerController getPlayerControlerCurrentTurn()
    {
        return Players[playerTurn - 1].GetComponent<PlayerController>();
    }
}
