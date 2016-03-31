using UnityEngine;
using System.Collections;

public class GameField : MonoBehaviour
{
    public static GameField instance;

    public GameObject GameTile;

    private GameObject[,] GameGrid;
    private GameObject selectedGameTile;

	// Use this for initialization
	void Start ()
    {
        instance = this;

        Debug.Log("start");
        int gridSizeX = GameSettings.Values.gridSizeX;
        int gridSizeZ = GameSettings.Values.gridSizeZ;
        int playerAreaX = GameSettings.Values.playerAreaX;
        int playerAreaZ = GameSettings.Values.PlayerAreaZ;

        GameGrid = new GameObject[gridSizeX, gridSizeZ];

        for (int xOffset = 0; xOffset < gridSizeX; xOffset++)
        {
            for (int zOffset = 0; zOffset < gridSizeZ; zOffset++)
            {
                float x = -(gridSizeX - 1) + 2 * xOffset;
                float y = 0.2F;
                float z = -(gridSizeZ - 1) + 2 * zOffset;

                GameObject newTile = (GameObject)Instantiate(GameTile, new Vector3(x, y, z), new Quaternion());
                newTile.transform.SetParent(this.transform);
                newTile.name = "GameTile[" + xOffset + "," + zOffset + "]";

                //assign tiles to player 1
                if (xOffset >= 0 && xOffset <= (playerAreaX - 1) && zOffset >= 0 && zOffset <= (playerAreaZ - 1))
                {
                    newTile.GetComponent<TileManager>().setUsabilityForPlayer(1, true);
                }
                //assign tiles to player 2
                else if (xOffset >= (gridSizeX - playerAreaX) && xOffset <= (gridSizeX - 1) && zOffset >= 0 && zOffset <= (playerAreaZ - 1))
                {
                    newTile.GetComponent<TileManager>().setUsabilityForPlayer(2, true);
                }
                //assign tiles to player 3
                else if (GameSettings.Values.numberOfPlayers >= 3 &&  xOffset >= 0 && xOffset <= (playerAreaX - 1) && zOffset >= (gridSizeZ - playerAreaZ) && zOffset <= (gridSizeZ - 1))
                {
                    newTile.GetComponent<TileManager>().setUsabilityForPlayer(3, true);
                }
                //assign tiles to player 4
                else if (GameSettings.Values.numberOfPlayers >= 4 && xOffset >= (gridSizeX - playerAreaX) && xOffset <= (gridSizeX - 1) && zOffset >= (gridSizeZ - playerAreaZ) && zOffset <= (gridSizeZ - 1))
                {
                    newTile.GetComponent<TileManager>().setUsabilityForPlayer(4, true);
                }

                GameGrid[xOffset, zOffset] = newTile;
            }
        }

        Debug.Log("done");
	}

    // Update is called once per frame
    void Update()
    {
        selectedGameTile = null;

        for (int x = 0; x < GameSettings.Values.gridSizeX; x++)
        {
            for (int z = 0; z < GameSettings.Values.gridSizeZ; z++)
            {
                if (GameGrid[x, z].GetComponent<TileManager>().getSelected())
                {
                    selectedGameTile = GameGrid[x, z];
                }
            }
        }
    }

    public void resetOccupiedUnderUnit(Rigidbody unit)
    {
        int gridSizeX = GameSettings.Values.gridSizeX;
        int gridSizeZ = GameSettings.Values.gridSizeZ;

        int gameGridX = ((gridSizeX - 1) + (int)unit.position.x) / 2;
        int gameGridZ = ((gridSizeZ - 1) + (int)unit.position.z) / 2;

        GameGrid[gameGridX, gameGridZ].GetComponent<TileManager>().setOccupied(false);

        Debug.Log(gameGridX + ", " + gameGridZ);
    }

    public GameObject getSelectedGameTile()
    {
        return selectedGameTile;
    }

    public void unSetAllMovable()
    {
        for (int x = 0; x < GameSettings.Values.gridSizeX; x++)
        {
            for (int z = 0; z < GameSettings.Values.gridSizeZ; z++)
            {
                GameGrid[x, z].GetComponent<TileManager>().setMoveTo(false);
            }
        }
    }

    public void unsetAllAttackable()
    {
        for (int x = 0; x < GameSettings.Values.gridSizeX; x++)
        {
            for (int z = 0; z < GameSettings.Values.gridSizeZ; z++)
            {
                GameGrid[x, z].GetComponent<TileManager>().setCanAttack(false);
            }
        }
    }


    public void deselectAllTiles()
    {
        for (int x = 0; x < GameSettings.Values.gridSizeX; x++)
        {
            for (int z = 0; z < GameSettings.Values.gridSizeZ; z++)
            {
                GameGrid[x, z].GetComponent<TileManager>().setSelected(false);
                selectedGameTile = null;
            }
        }
    }
}
