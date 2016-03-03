using UnityEngine;
using System.Collections;

public class GameField : MonoBehaviour
{
    public GameObject GameTile;

    private GameObject[,] GameGrid;
    private GameObject selectedGameTile;

	// Use this for initialization
	void Start ()
    {
        Debug.Log("start");

        GameGrid = new GameObject[20, 20];

        for (int xOffset = 0; xOffset < 20; xOffset++)
        {
            for (int yOffset = 0; yOffset < 20; yOffset++)
            {
                float x = -19 + 2 * xOffset;
                float y = 0.2F;
                float z = -19 + 2 * yOffset;

                GameGrid[xOffset, yOffset] = (GameObject)Instantiate(GameTile, new Vector3(x, y, z), new Quaternion());
            }
        }

        Debug.Log("done");
	}

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                if (GameGrid[i, j].GetComponent<TileManager>().getSelected())
                {
                    selectedGameTile = GameGrid[i, j];
                }
            }
        }
    }

    public GameObject getSelectedGameTile()
    {
        return selectedGameTile;
    }
   

    public void deselectAll()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                GameGrid[i, j].GetComponent<TileManager>().deselectTile();
                selectedGameTile = null;
            }
        }
    }
}
