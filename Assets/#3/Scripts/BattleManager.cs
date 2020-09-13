using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] ShipController[] ships;
    [SerializeField] GameObject[] shipInventoryPanels;
    [SerializeField] GameObject shipEquipmentPanel;

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
}
