using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MainGame : MonoBehaviour
{
    public static MainGame instance;

    public GameObject PlayerContainer;
    public GameField GameField;

    private List<GameObject> Players;
    private int turn;

	// Use this for initialization
	void Start ()
    {
        instance = this;

        Players = new List<GameObject>();

        //Add players to the list of available players
        for (int i = 0; i < PlayerContainer.transform.childCount; i++)
        {
            Debug.Log(i);
            Players.Add(PlayerContainer.transform.GetChild(i).gameObject);
        }

        //Make a random player start the game
        turn = Random.Range(1, GameSettings.Values.numberOfPlayers);
        CameraControl.instance.setCameraPositionForPlayer(turn);
        getPlayerControlerCurrentTurn().giveTurn();  
	}
	
	// Update is called once per frame
	void Update ()
    {
        GUIManager.instance.setPlayerTurnGUI(turn);

        if (Input.GetMouseButtonDown(0))
        {
            //prevent raycast from going through buttons
            if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    //Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object

                    if (hit.transform.tag == "GameTile")
                    {
                        deselectAllItems();
                        hit.collider.gameObject.GetComponent<TileManager>().selectTile();
                        GUIManager.instance.showUnitButtons(false);
                    }
                    else if(hit.transform.tag == "Unit")
                    {
                        deselectAllItems();
                        hit.collider.gameObject.GetComponent<UnitController>().selectUnit();

                        if (hit.collider.gameObject.GetComponent<UnitController>().belongsToPlayer(turn))
                        {        
                            GUIManager.instance.showUnitButtons(true);
                        }  
                    }
                }
            }     
        }
    }

    //handling buttons for buying units
    public void buyUnitFromButton(string unit)
    {
        GameObject selectedGameTile = GameField.getSelectedGameTile();
        //Debug.Log(selectedGameTile);
        if (selectedGameTile != null)
        {
            //Debug.Log(selectedGameTile.GetComponent<TileManager>().getOccupied());
            //Debug.Log(selectedGameTile.GetComponent<TileManager>().getUsabiltyForPlayer(turn));
            if (!selectedGameTile.GetComponent<TileManager>().getOccupied()
                && selectedGameTile.GetComponent<TileManager>().getUsabiltyForPlayer(turn))
            { 
                if (getPlayerControlerCurrentTurn().buyUnit(unit, selectedGameTile, turn))
                {
                    //nextTurn();
                }
            }
        }
    }

    public void nextTurn()
    {
        GameField.deselectAll();

        getPlayerControlerCurrentTurn().endTurn();
        turn++;
        if (turn > GameSettings.Values.numberOfPlayers)
        {
            turn = 1;
        }
        CameraControl.instance.setCameraPositionForPlayer(turn);
        getPlayerControlerCurrentTurn().giveTurn();
    }

    private PlayerController getPlayerControlerCurrentTurn()
    {
        return Players[turn - 1].GetComponent<PlayerController>();
    }

    private void deselectAllItems()
    {
        GameField.deselectAll();
        foreach (GameObject o in Players)
        {
            o.GetComponent<PlayerController>().deselectAll();
        }
    }
}
