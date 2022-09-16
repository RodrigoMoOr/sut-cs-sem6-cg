using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public enum BattleState
{
    START,
    ACTION_SELECTION,
    MOVE_SELECTION,
    BUSY,
    END
}

public class BattleSystem : MonoBehaviour
{


    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;


    [SerializeField] GameObject background;

    [SerializeField] BattleDialogBox dialogBox;




    public event Action OnBattleQuit;

    public BattleState battleState;





    MoveBase chosenMove;

    bool turnRunning = false;




    public void StartBattle(Boss boss)
    {
        StartCoroutine(SetupBattle(boss));
    }

    public IEnumerator SetupBattle(Boss boss)
    {

        playerUnit.Clear();
        enemyUnit.Clear();


        playerUnit.Setup();
        enemyUnit.Setup(boss);

        background.GetComponent<Image>().sprite = boss.Base.BattleBackground;

        yield return  dialogBox.TypeDialog($"Your professor {boss.Base.Name} appeared!");
        ActionSelection();
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
    }

    void ActionSelection()
    {
        battleState = BattleState.ACTION_SELECTION;

        dialogBox.EnableMoveSelector(false);
        dialogBox.EnableDialogText(true);
        dialogBox.EnableActionSelector(true);
    }
    void MoveSelection()
    {
        battleState = BattleState.MOVE_SELECTION;

        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }



    void BusyDisplay()
    {
        battleState = BattleState.BUSY;
        dialogBox.EnableMoveSelector(false);
        dialogBox.EnableDialogText(true);
        dialogBox.EnableMoveSelector(false);
    }


    void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            dialogBox.IncrementSelection();
            dialogBox.UpdateActionSelector();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            dialogBox.DecrementSelection();
            dialogBox.UpdateActionSelector();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            //fight
            if (dialogBox.GetChoice() == 0)
            {
                dialogBox.UpdateMoveData();
                MoveSelection();
            }
            //Run
            else if (dialogBox.GetChoice() == 1)
            {
                OnBattleQuit();
            }
            dialogBox.ResetChoice();
        }
    }


    void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            dialogBox.IncrementSelection();
            dialogBox.UpdateMoveData();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            dialogBox.DecrementSelection();
            dialogBox.UpdateMoveData();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        { 
                chosenMove = PlayerStats.Instance.Move[dialogBox.GetChoice()];
                if(!turnRunning)
                StartCoroutine(MakeTurns());
                dialogBox.ResetChoice();
        }

       
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActionSelection();
        }
    }

    private IEnumerator MakeTurns()
    {
        BusyDisplay();
        turnRunning = true;
        ///////////////PLAYER TURN///////////////////
        int damageToDeal = chosenMove.Damage;
        bool isDead = enemyUnit.GetDamage(damageToDeal);

        yield return dialogBox.TypeDialog($"You used attack: {chosenMove.Name}!");
        yield return BattleHud.ShowAttackSprite(chosenMove.MoveSprite);
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            StartCoroutine(HandleWin());
            turnRunning = false;
        }
        else
        {
            ///////////////BOSS TURN/////////////////////
            AttackBase chosenAttack = enemyUnit.Boss.Base.Attack();
            damageToDeal = chosenAttack.Damage;

            bool isPlayerDead = playerUnit.GetDamage(damageToDeal);

            yield return new WaitForSeconds(1f);
            yield return dialogBox.TypeDialog($"{enemyUnit.Boss.Base.Name} used attack: {chosenAttack.Name}!");
            yield return BattleHud.ShowAttackSprite(chosenAttack.AttackSprite);
            yield return new WaitForSeconds(2f);



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
        yield return dialogBox.TypeDialog("You lost the battle!!");
        playerUnit.PlayDieAnimation();
        yield return new WaitForSeconds(5f);
        //SOMETHING HERE SHOULD BE??
        //Yees. To a complete stop the game should come here
        
        yield return SceneManager.LoadSceneAsync(0);
        OnBattleQuit();
    }

    private IEnumerator HandleWin()
    {
        yield return dialogBox.TypeDialog("You Won the battle!!");
        enemyUnit.PlayDieAnimation();
        yield return new WaitForSeconds(2f);
        GameObject.Find("Boss").SetActive(false);
        PlayerStats.Instance.AddECTS(30);
        PlayerStats.Instance.AddMaxHP(100);
        PlayerStats.Instance.AddHP(100000);
        PlayerStats.Instance.Reached = SceneManager.GetActiveScene().buildIndex + 1;
        OnBattleQuit();

    }

}

