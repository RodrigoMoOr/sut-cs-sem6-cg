using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{

    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogBox battleDialogBox;
    [SerializeField] UlaBoss boss;

    public event Action onBattleQuit;



    public void Start()
    {
        SetupBattle();
    }


    public void SetupBattle()
    {
        playerHud.SetData();
        enemyHud.SetData();

        StartCoroutine(battleDialogBox.TypeDialog("Wild boss appeared!"));

    }


    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            battleDialogBox.IncrementSelection();
            battleDialogBox.UpdateActionSelector();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            battleDialogBox.DecrementSelection();
            battleDialogBox.UpdateActionSelector();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if(battleDialogBox.GetChoice() == 1)
            {
                onBattleQuit();
            }
            
        }



    }
}

