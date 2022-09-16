using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
    [SerializeField] Text dialogText;
    [SerializeField] GameObject dialogTextObject;
    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;
    [SerializeField] List<Text> actionText;
    [SerializeField] Text moveText;
    [SerializeField] List<MoveBase> movesBase;
    [SerializeField] Color selectionColor;
    [SerializeField] Text damageText;



    public void SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }


    
    private int choice;
    public void IncrementSelection()
    {
        choice++;
    }
    public void DecrementSelection()
    {
        choice--;
    }


    public void UpdateActionSelector()
    {
        choice = Mathf.Clamp(choice, 0, actionText.Count - 1);
        for (int i = 0; i < actionText.Count; i++)
        {
            if (i == choice)
                actionText[i].color = selectionColor;
            else
                actionText[i].color = Color.black;
        }
    }


    public void UpdateMoveData()
    {
        choice %= movesBase.Count;
        if (choice < 0)
        {
            choice = movesBase.Count - 1;
        }
        moveText.text = movesBase[choice].Name;
        damageText.text = $"DMG:{movesBase[choice].Damage}";
    }

    
    public int GetChoice()
    {
        return choice;
    }
    public void ResetChoice()
    {
        choice = 0;
    }

    public IEnumerator TypeDialog(string dialog)
    {
        
        dialogText.text = "";
        foreach (char letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        SetDialog(dialog);
    }


    public void EnableDialogText(bool enabled)
    {
        dialogTextObject.SetActive(enabled);
        dialogText.enabled = enabled;

    }

    public void EnableActionSelector(bool enabled)
    {
        actionSelector.SetActive(enabled);
    }
    public void EnableMoveSelector(bool enabled)
    {
        movesBase = PlayerStats.Instance.Move;
        moveSelector.SetActive(enabled);
    }


}
