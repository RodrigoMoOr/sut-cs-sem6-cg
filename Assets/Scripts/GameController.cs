using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState {FreeRoam, Battle, Dialog }


public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    UlaBoss boss;

   

    GameState gameState;


    private void Start()
    {
        playerController.onEncounter += StartBattle;
        battleSystem.onBattleQuit += ExitBattle;
    }



    void StartBattle()
    {
        gameState = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);
    }



    void ExitBattle()
    {
        gameState = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }

    


    private void Update()
    {
        if (gameState == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }

        else if (gameState == GameState.Battle)
        {
            battleSystem.HandleUpdate();
        }
        else if (gameState == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
    }


}
