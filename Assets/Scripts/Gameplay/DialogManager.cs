using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] float lettersPerSecond;


    public event Action onDialogStart;
    public event Action onDialogEnd;

    Dialog dialog;
    int dialogIndex = 0;
    bool isBusyTyping = false;
    
    public static DialogManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }


    public IEnumerator ShowDialog(Dialog dialog)
    {
        yield return new WaitForEndOfFrame();
        
        onDialogStart?.Invoke();
        
        this.dialog = dialog;
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }


    public void LogDialog(string log)
    {
        dialogBox.SetActive(true);
        dialogText.text = log;
        StartCoroutine(TypeDialog(log));
        
    }

    public IEnumerator TypeDialog(string line)
    {
        isBusyTyping = true;
        dialogText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isBusyTyping = false;
    }



    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isBusyTyping)
        {
            dialogIndex++;
            if (dialogIndex < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[dialogIndex]));
            }
            else
            {
                dialogIndex = 0;
                dialogBox.SetActive(false);
                onDialogEnd?.Invoke();
            }
        }
    }
    
}
