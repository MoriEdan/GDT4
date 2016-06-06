using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MainGame : MonoBehaviour
{
    public static MainGame instance;

    public GameObject PlayerContainer;
    public GameObject Player;

    private List<GameObject> Players;
    private int turn;
    

    // Use this for initialization
    void Start ()
    {
        instance = this;
        Players = new List<GameObject>();

        //Add players to the list of available players
        for (int i = 0; i < GameSettings.Values.numberOfPlayers; i++)
        {
            Debug.Log(i);
            GameObject newPlayer = (GameObject)Instantiate(Player, new Vector3(0, 0, 0), new Quaternion());
            newPlayer.transform.SetParent(PlayerContainer.transform);
            newPlayer.name = "Player " + (i + 1).ToString();
            Players.Add(newPlayer);
        }

        //Make a random player start the game
        turn = Random.Range(1, GameSettings.Values.numberOfPlayers);
        CameraControl.instance.setCameraPositionForPlayer(turn);
        getPlayerControlerCurrentTurn().giveTurn();  
	}

    // Update is called once per frame
    void Update()
    {
        GUIManager.instance.setPlayerTurnGUI(turn);

        if (Input.GetMouseButtonDown(0))
        {
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object

                    if (hit.transform.tag == "GameTile")
                    {
                        GameObject selectedTile = GameField.instance.getSelectedGameTile();
                        GameObject selectedUnit = getPlayerControlerCurrentTurn().getSelectedUnit();

                        //select new tile
                        if (selectedTile == null && selectedUnit == null)
                        {
                            GUIManager.instance.showUnitButtons(false);
                            hit.collider.gameObject.GetComponent<TileManager>().setSelected(true);
                        }
                        //move unit
                        else if (selectedTile == null && selectedUnit != null)
                        {
                            if (hit.collider.gameObject.GetComponent<TileManager>().getCanMoveTo()
                                    && !selectedUnit.GetComponent<UnitController>().getHasMoved())
                            {
                                GameField.instance.resetOccupiedUnderUnit(selectedUnit);
                                selectedUnit.GetComponent<UnitController>().moveUnit(selectedUnit, hit.collider.gameObject);
                                removeUnitHighlights();
                            }
                        }
                        //select new tile
                        else if (selectedTile != null && selectedUnit == null)
                        {
                            GameField.instance.deselectAllTiles();
                            GUIManager.instance.showUnitButtons(false);
                            hit.collider.gameObject.GetComponent<TileManager>().setSelected(true);
                        }
                        else if (selectedTile != null && selectedUnit != null)
                        {
                            Debug.Log("this shouldnt happen");
                        }
                    }
                    else if (hit.transform.tag == "Unit")
                    {
                        GameObject selectedUnit = getPlayerControlerCurrentTurn().getSelectedUnit();

                        if (selectedUnit == null)
                        {
                            if (hit.collider.gameObject.GetComponent<UnitController>().belongsToPlayer(turn))
                            {
                                GameField.instance.deselectAllTiles();
                                hit.collider.gameObject.GetComponent<UnitController>().selectUnit();
                                GUIManager.instance.showUnitButtons(true);
                                Debug.Log(hit.collider.gameObject.GetComponent<UnitController>().ToString());
                            }
                        }
                        else if (selectedUnit != null)
                        {
                            Debug.Log(selectedUnit);
                            Debug.Log(hit.collider.gameObject.GetComponent<Rigidbody>());

                            if (selectedUnit != hit.collider.gameObject.GetComponent<Rigidbody>() && hit.collider.gameObject.GetComponent<UnitController>().getCanBeAttacked()
                                && !selectedUnit.GetComponent<UnitController>().getHasAttacked())
                            {
                                removeAllHighlights();
                                selectedUnit.GetComponent<UnitController>().attackWithUnit(hit.collider.gameObject.GetComponent<Rigidbody>());            
                            }
                        }
                    }
                }
            }
        }
    }

    //deselectUnit and remove movement mark and hide buttons
    public void cancelUnitFromButton()
    {
        removeUnitHighlights();
        foreach (GameObject player in Players)
        {
            player.GetComponent<PlayerController>().deselectAllUnits();
        }
        GUIManager.instance.showUnitButtons(false);
    }

    //handling buttons for buying units
    public void buyUnitFromButton(string unit)
    {
        GameObject selectedGameTile = GameField.instance.getSelectedGameTile();
        if (selectedGameTile != null)
        {
            if (!selectedGameTile.GetComponent<TileManager>().getOccupied()
                && selectedGameTile.GetComponent<TileManager>().getUsabiltyForPlayer(turn))
            { 
                if (!getPlayerControlerCurrentTurn().buyUnit(unit, selectedGameTile, turn))
                {
                    Debug.Log("buying unit failed");
                }
            }
        }
    }

    public void nextTurn()
    {
        removeAllHighlights();

        //Increment turn
        getPlayerControlerCurrentTurn().endTurn();

        bool foundActivePlayer = false;
        int numberOfPlayersStillPlaying = GameSettings.Values.numberOfPlayers;

        while (!foundActivePlayer)
        {
            turn++;
            if(turn > GameSettings.Values.numberOfPlayers)
            {
                turn = 1;
            }

            if (!getPlayerControlerCurrentTurn().checkLost())
            {
                foundActivePlayer = true;
            }
            else
            {
                numberOfPlayersStillPlaying--;
            }
        }

        if(numberOfPlayersStillPlaying == 1)
        {
            GUIManager.instance.displayPlayerWin(turn);
        }
        else
        {
            //methodes to set the new turn
            CameraControl.instance.setCameraPositionForPlayer(turn);
            getPlayerControlerCurrentTurn().giveTurn();
        } 
    }

    public int getCurrentTurnForArray()
    {
        return turn - 1;
    }

    public PlayerController getPlayerControlerCurrentTurn()
    {
        return Players[getCurrentTurnForArray()].GetComponent<PlayerController>();
    }

    private void removeUnitHighlights()
    {
        GameField.instance.unSetAllMovable();
        GameField.instance.unsetAllAttackable();
    }

    private void removeAllHighlights()
    {
        removeUnitHighlights();
        GameField.instance.deselectAllTiles();
        foreach (GameObject player in Players)
        {
            player.GetComponent<PlayerController>().deselectAllUnits();
        }
    }
}
