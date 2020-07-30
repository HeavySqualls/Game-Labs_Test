using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] ShipController[] ships;

    public void StartBattle()
    {
        Debug.Log("Starting Battle");

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
