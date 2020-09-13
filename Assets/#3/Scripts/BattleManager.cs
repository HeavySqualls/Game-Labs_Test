using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public bool isBattleOver = false;
    public TextMeshProUGUI outcomeText;

    ShipController winner;
    ShipController looser;

    [SerializeField] ShipController[] ships;
    [SerializeField] GameObject[] shipInventoryPanels;
    [SerializeField] GameObject shipEquipmentPanel;
    [SerializeField] GameObject gameCanvas;

    public void StartBattle()
    {
        Debug.Log("Starting Battle");

        // Disable the equipment UI panels 
        foreach (GameObject ui in shipInventoryPanels)
        {
            ui.SetActive(false);
        }

        shipEquipmentPanel.SetActive(false);

        // TODO: Add in a check to make sure both ships are ready to go 
        for (int i = 0; i < ships.Length; i++)
        {
            if (ships[i] == ships[0])
            {
                ships[i].BeginCombat(ships[1].gameObject);
            }
            else if (ships[i] == ships[1])
            {
                ships[i].BeginCombat(ships[0].gameObject);
            }
        }
    }

    public void EndBattle()
    {
        isBattleOver = true;

        foreach (ShipController ship in ships)
        {
            if (ship.isShipDead)
            {
                looser = ship;
                Debug.Log(ship.name + " has been defeated!");
            }
            else
            {
                winner = ship;
                Debug.Log(ship.name + " is victorious!");
            }
        }

        outcomeText.text = winner.name + " is victorious!";

        gameCanvas.SetActive(true);
    }
}
