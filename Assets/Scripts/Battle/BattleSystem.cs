using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public enum BattleState
{
    START,
    ACTION_SELECTION,
    MOVE_SELECTION,
    PLAYER_TURN,
    ENEMY_TURN,
    END
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
    bool turnRunning = false;

    public void Start()
    {
        SetupBattle();

    }


    public void SetupBattle()
    {
        battleState = BattleState.START;
        playerHud.SetData();
        enemyHud.SetData(boss);


        StartCoroutine(battleDialogBox.TypeDialog($"Your professor {boss.Name} appeared!"));
        battleState = BattleState.ACTION_SELECTION;
    }


    public void HandleUpdate()
    {
        if (battleState == BattleState.ACTION_SELECTION)
        {
            HandleActionSelection();
        }
        else if (battleState == BattleState.MOVE_SELECTION)
        {
            HandleMoveSelection();
        }
        // else if (battleState == BattleState.PLAYER_TURN)
        // {
        //     HandlePlayerTurn();
        // }
        // else if (battleState == BattleState.ENEMY_TURN)
        // {
        //     HandleEnemyTurn();
        // }
        //else if (battleState == BattleState.END)
        // {

        // }
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
                if(!turnRunning)
                    StartCoroutine(MakeTurns());
            }

        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActionSelection();
        }
    }

    private IEnumerator MakeTurns()
    {
        turnRunning = true;
        ///////////////PLAYER TURN///////////////////
        int damageToDeal = selectedMove.Damage;
        bool isDead = boss.GetDamage(damageToDeal);

        StartCoroutine(BattleHud.ShowAttackSprite(selectedMove.MoveSprite));
        yield return new WaitForSeconds(2f);
        StartCoroutine(enemyHud.LowerHealthBar(boss.HealthProcent));


        if (isDead)
        {
            StartCoroutine(HandleWin());
            turnRunning = false;
        }
        else
        {
            ///////////////BOSS TURN/////////////////////
            AttackBase chosenAttack = boss.Attack();
            damageToDeal = chosenAttack.Damage;

            bool isPlayerDead = PlayerStats.Instance.GetDamage(damageToDeal);

            yield return new WaitForSeconds(1f);
            StartCoroutine(BattleHud.ShowAttackSprite(chosenAttack.AttackSprite));
            yield return new WaitForSeconds(2f);
            StartCoroutine(playerHud.LowerHealthBar(PlayerStats.Instance.HealthProcent));



            if (isPlayerDead)
            {
                StartCoroutine(HandleLose());
                yield break;
            }
            ActionSelection();
            turnRunning = false;
        }
    }

    private IEnumerator HandleLose()
    {
        StartCoroutine(battleDialogBox.TypeDialog("You Lost the battle!!"));
        StartCoroutine(playerHud.Kill());
        yield return new WaitForSeconds(2f);
        //SOMETHING HERE SHOULD BE??
        OnBattleQuit();
    }

    private IEnumerator HandleWin()
    {
        StartCoroutine(battleDialogBox.TypeDialog("You Won the battle!!"));
        StartCoroutine(enemyHud.Kill());
        yield return new WaitForSeconds(2f);
        boss.gameObject.SetActive(false);
        PlayerStats.Instance.AddECTS(30);
        OnBattleQuit();

    }

    public void SetBossData(Boss bossg)
    {
        boss = bossg;
    }
}

