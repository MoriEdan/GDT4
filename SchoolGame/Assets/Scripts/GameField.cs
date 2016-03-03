using UnityEngine;
using System.Collections;

public class GameField : MonoBehaviour
{
    public GameObject GameTile;

    private GameObject[,] GameGrid;

	// Use this for initialization
	void Start ()
    {
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
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
