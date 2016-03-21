using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour
{
    public static GUIManager instance;

    public GameObject UnitButtons;
    public Text PlayerTurn;
    public GameObject ResourceContainer;

    // Use this for initialization
    void Start ()
    {
        instance = this;
        UnitButtons.SetActive(false);
    }

    public void showUnitButtons(bool active)
    {
        UnitButtons.SetActive(active);
    }
	
	public void setPlayerTurnGUI(int turn)
    {
        PlayerTurn.text = "Player " + turn + " Turn!";
    }

    public void updateResourcesGUI(int foodCount, int goldCount, int unitCount, int unitCap)
    {
        Text[] Resources = ResourceContainer.GetComponentsInChildren<Text>();

        Resources[0].text = "Food: " + foodCount;
        Resources[1].text = "Gold: " + goldCount;
        Resources[2].text = "Units: " + unitCount + "/" + unitCap;
    }

    public void endTurnButton()
    {
        UnitButtons.SetActive(false);
        MainGame.instance.nextTurn();
    }

    public void buyUnitButton(string unit)
    {
        MainGame.instance.buyUnitFromButton(unit);
    }

    public void unitMoveButton()
    {
        //MainGame.instance.moveUnit();
    }

    public void unitAttackButton()
    {

    }
}
