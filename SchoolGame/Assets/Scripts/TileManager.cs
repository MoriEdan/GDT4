using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour {

    private bool selected;
    private int occupied;

    // Use this for initialization
    void Start()
    {
        occupied = 0;   //0 means tile is free
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            //do something
        }
    }
}
