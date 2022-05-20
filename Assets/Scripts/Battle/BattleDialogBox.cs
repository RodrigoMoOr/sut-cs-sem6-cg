using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
    [SerializeField] Text dialogText;
    [SerializeField] GameObject actionSelector;
    [SerializeField] List<Text> actionText;
    [SerializeField] Color selectionColor;
    // Start is called before the first frame update
    public void SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }


    
    private int choice;
    public void IncrementSelection()
    {
        choice++;
        Debug.Log("Inc= " + choice);
        choice = Mathf.Abs(choice) % 2;
        Debug.Log("Mod = " + choice);
    }
    public void DecrementSelection()
    {
        choice--;
        choice =  Mathf.Abs(choice) % 2;
    }


    public void UpdateActionSelector()
    {
        for (int i = 0; i < actionText.Count; i++)
        {
            if (i == choice)
                actionText[i].color = selectionColor;
            else
                actionText[i].color = Color.black;
        }
    }

    
    public int GetChoice()
    {
        return choice;
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
}
