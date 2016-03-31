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

    public string currentToolTipText;
    private GUIStyle guiStyleFore;
    private GUIStyle guiStyleBack;

    // Use this for initialization
    void Start ()
    {
        instance = this;
        UnitButtons.SetActive(false);

        currentToolTipText = "";

        guiStyleFore = new GUIStyle();
        guiStyleFore.normal.textColor = Color.magenta;
        guiStyleFore.fontSize = 20;
        guiStyleFore.alignment = TextAnchor.UpperLeft;
        guiStyleFore.wordWrap = true;

        guiStyleBack = new GUIStyle();
        guiStyleBack.normal.textColor = Color.black;
        guiStyleBack.fontSize = 20;
        guiStyleBack.alignment = TextAnchor.UpperLeft;
        guiStyleBack.wordWrap = true;
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

    public void unitCancelButton()
    {
        MainGame.instance.cancelUnitFromButton();
    }

    public void OnGUI()
    {
        if (currentToolTipText != "")
        {
            var x = Event.current.mousePosition.x;
            var y = Event.current.mousePosition.y;
            GUI.Label(new Rect(x - 9, y + 20, 300, 60), currentToolTipText, guiStyleBack);
            GUI.Label(new Rect(x - 10, y + 20, 300, 60), currentToolTipText, guiStyleFore);
        }
    }
}