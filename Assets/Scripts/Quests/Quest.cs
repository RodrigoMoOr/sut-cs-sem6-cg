using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestBase Base { get; private set; }
    public QuestStatus Status { get; private set; }

    public Quest(QuestBase baseQuest)
    {
        Base = baseQuest;
    }


    public IEnumerator StartQuest()
    {
        Status = QuestStatus.Started;

        yield return DialogManager.Instance.ShowDialog(Base.StartDialogue);
    }


    public IEnumerator CompleteQuest(Transform player)
    {
        Status = QuestStatus.Completed;

        yield return DialogManager.Instance.ShowDialog(Base.CompletedDialogue);

        var inventory = Inventory.GetInventory();
        
        if(Base.RequiredItem != null)
        {
            inventory.RemoveItem(Base.RequiredItem);
        }
        
        if(Base.RewardItem != null)
        {
            inventory.AddItem(Base.RewardItem);

            string playerName = player.GetComponent<PlayerStats>().Name;

            yield return DialogManager.Instance.ShowDialogText($"{playerName} received{Base.RewardItem.Name}");
        }
    }


    public bool CanBeCompleted()
    {
        if (Base.RequiredItem != null)
        {
            var inventory = Inventory.GetInventory();
            if (inventory.Slots.Find(slot => slot.Item == Base.RequiredItem) == null)
            {
                return false;
            }
        }
        return true;
    }
    
    
    //public bool HasItem(ItemBase item)
    //{
        
    //}

}

public enum QuestStatus { None, Started, Completed};


