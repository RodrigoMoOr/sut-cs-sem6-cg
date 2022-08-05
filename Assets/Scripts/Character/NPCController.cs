using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{

    [SerializeField] Dialog dialog;

    [SerializeField] QuestBase questToStart;
    Quest activeQuest;
    
    public IEnumerator Interact(Transform initiator)
    {
        if (questToStart != null)
        {

            activeQuest = new Quest(questToStart);
            yield return activeQuest.StartQuest();
            questToStart = null;
        }
        else if (activeQuest != null)
        {
            if (activeQuest.CanBeCompleted())
            {
                yield return activeQuest.CompleteQuest(initiator);
                activeQuest = null;
            }
            else
            {
                yield return DialogManager.Instance.ShowDialog(activeQuest.Base.InProgressDialogue);
            }
        }
        else
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
        }
    }
}  
