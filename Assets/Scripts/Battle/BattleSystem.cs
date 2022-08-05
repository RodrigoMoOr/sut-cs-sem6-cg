using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BattleState
{
    START,
    ACTION_SELECTION,
    MOVE_SELECTION,
    PLAYER_TURN,
    ENEMY_TURN,
    WON,
    LOST
}

public class BattleSystem : MonoBehaviour
{

    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    
    [SerializeField] BattleDialogBox battleDialogBox;
    
    //Bosss passed in GameController.cs with SetBossData() function here
    [SerializeField] Boss boss;

    public event Action OnBattleQuit;

    public BattleState battleState;

    MoveBase selectedMove;

    public void Start()
    {
        SetupBattle();
        
    }


    public void SetupBattle()
    {
        battleState = BattleState.START;
        playerHud.SetData();
        enemyHud.SetData(boss);


        StartCoroutine(battleDialogBox.TypeDialog("Wild boss appeared!"));
        battleState = BattleState.ACTION_SELECTION;
    }


    public void HandleUpdate()
    {
       if(battleState == BattleState.ACTION_SELECTION)
        {
            HandleActionSelection();
        }
        else if (battleState == BattleState.MOVE_SELECTION)
        {
            HandleMoveSelection();
        }
        else if (battleState == BattleState.PLAYER_TURN)
        {
            HandlePlayerTurn();
        }
        else if (battleState == BattleState.ENEMY_TURN)
        {
            HandleEnemyTurn();
        }
        else if (battleState == BattleState.WON)
        {
            HandleWin();
        }
        else if (battleState == BattleState.LOST)
        {
            //HandleLose();
        }
    }

    void ActionSelection()
    {
        battleState = BattleState.ACTION_SELECTION;
        battleDialogBox.EnableMoveSelector(false);
        battleDialogBox.EnableDialogText(true);
        battleDialogBox.EnableActionSelector(true);
    }
    void MoveSelection()
    {
        battleState = BattleState.MOVE_SELECTION;
        battleDialogBox.EnableActionSelector(false);
        battleDialogBox.EnableDialogText(false);
        battleDialogBox.EnableMoveSelector(true);
    }

   

    void PlayerTurn()
    {
        battleState = BattleState.PLAYER_TURN;
        battleDialogBox.EnableMoveSelector(false);
        battleDialogBox.EnableDialogText(true);
        battleDialogBox.EnableMoveSelector(false);
    }

    void EnemyTurn()
    {
        battleState = BattleState.ENEMY_TURN;
    }
    

    void HandleActionSelection()
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
            //Fight
            if (battleDialogBox.GetChoice() == 0)
            {
                battleDialogBox.UpdateMoveTexts();
                MoveSelection();
            }
            //Run
            else if (battleDialogBox.GetChoice() == 1)
            {
                OnBattleQuit();
            }
        }
    }


    void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            battleDialogBox.IncrementSelection();
            battleDialogBox.UpdateMoveSelector();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            battleDialogBox.DecrementSelection();
            battleDialogBox.UpdateMoveSelector();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            //Fight
            if (battleDialogBox.GetChoice() == 0)
            {
                Debug.Log("Move1 TODO");
                selectedMove = PlayerStats.Instance.Move[battleDialogBox.GetChoice()];
                PlayerTurn();
            }

        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActionSelection();
        }
    }

    void HandlePlayerTurn()
    {
        int damageToDeal = selectedMove.Damage; 
        if(boss.GetDamage(damageToDeal))
        {
            battleState = BattleState.WON;
            return;
        }
        EnemyTurn();
    }
    
    void HandleEnemyTurn()
    {
        AttackBase chosenAttack = boss.Attack();
        int damageToDeal = chosenAttack.Damage;
        //Sprite attackSprite = chosenAttack.AttackSprite;

        if(PlayerStats.Instance.GetDamage(damageToDeal))
        {
            battleState = BattleState.LOST;
        }
        ActionSelection();
    }

    void HandleWin()
    {
        Debug.Log("Win!!!!!");
        boss.gameObject.SetActive(false);
        PlayerStats.Instance.AddECTS(30);
        OnBattleQuit();
    }

    public void SetBossData(Boss bossg)
    {
        boss = bossg;
    }
}

