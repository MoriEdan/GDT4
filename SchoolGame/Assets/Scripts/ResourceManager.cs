using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    private int foodCount;
    private int goldCount;
    private int unitCount;
    private int unitCap;

    public GameObject ResourceContainer;


	// Use this for initialization
	void Start ()
    {
        //init resources
        foodCount = 100;
        goldCount = 100;
        unitCount = 0;
        unitCap = 5;
	}
	
	// Update is called once per frame
	void Update ()
    {
        updateResources();
    }

    private void updateResources()
    {
        Text[] Resources = ResourceContainer.GetComponentsInChildren<Text>();

        Resources[0].text = "Food: " + foodCount;
        Resources[1].text = "Gold: " + goldCount;
        Resources[2].text = "Units: " + unitCount + "/" + unitCap;
    }
}
