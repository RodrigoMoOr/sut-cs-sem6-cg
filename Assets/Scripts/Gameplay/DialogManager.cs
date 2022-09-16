using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] GameObject dialogImage;
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


    public IEnumerator ShowDialogText(string text, Sprite dialogSprite = null, bool waitForInput = true, bool autoClose = true,
    List<string> choices = null, Action<int> onChoiceSelected = null)
    {
        onDialogStart?.Invoke();
        dialogBox.SetActive(true);

        if (dialogSprite != null)
        {
            dialogImage.SetActive(true);
            dialogImage.GetComponent<Image>().sprite = dialogSprite;
        }



        yield return TypeDialog(text);
        if (waitForInput)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        }

        //if (choices != null && choices.Count > 1)
        //{
        //    yield return choiceBox.ShowChoices(choices, onChoiceSelected);
        //}

        if (autoClose)
        {
            CloseDialog();
        }
        onDialogEnd?.Invoke();
    }

    public void CloseDialog()
    {
        dialogBox.SetActive(false);
        dialogImage.SetActive(false);
    }


    public IEnumerator ShowDialog(Dialog dialog, Sprite dialogSprite = null)
    {
        yield return new WaitForEndOfFrame();
        
        onDialogStart?.Invoke();
        



        this.dialog = dialog;
        dialogBox.SetActive(true);

        if (dialogSprite != null)
        {
            dialogImage.SetActive(true);
            dialogImage.GetComponent<Image>().sprite = dialogSprite;
        }


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
                dialogImage?.SetActive(false);
                onDialogEnd?.Invoke();
            }
        }
    }
    
}
