using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MainGame : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public GameField GameField;

    public Text PlayerTurn;

    private List<GameObject> Players;
    private int maxNumPlayers;
    private int turn;

	// Use this for initialization
	void Start ()
    {
        //Add players to the list of available players
        Players = new List<GameObject>();
        Players.Add(Player1);
        Players.Add(Player2);

        //Make a random player start the game
        maxNumPlayers = 2;
        turn = Random.Range(1, maxNumPlayers);
        getPlayerControlerCurrentTurn().giveTurn();
	}
	
	// Update is called once per frame
	void Update ()
    {
        PlayerTurn.text = "Player " + turn + " turn!";
	}

    public void nextTurn()
    {
        getPlayerControlerCurrentTurn().endTurn();
        turn++;
        if(turn > maxNumPlayers)
        {
            turn = 1;
        }
        getPlayerControlerCurrentTurn().giveTurn();
    }

    public void buyUnit(string unit)
    {
        if(getPlayerControlerCurrentTurn().buyUnit(unit))
        {
            nextTurn();
        }
    }

    private PlayerController getPlayerControlerCurrentTurn()
    {
        return Players[turn - 1].GetComponent<PlayerController>();
    }
}
