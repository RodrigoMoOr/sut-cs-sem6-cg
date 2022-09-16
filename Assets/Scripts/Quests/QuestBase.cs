using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Quests/Create new quest")]
public class QuestBase : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] string description;

    [SerializeField] Dialog startDialogue;
    [SerializeField] Dialog inProgressDialogue;
    [SerializeField] Dialog completedDialogue;

    [SerializeField] ItemBase requiredItem;
    [SerializeField] ItemBase rewardItem;

    public string Name => name;

    public string Description => description;

    public Dialog StartDialogue => startDialogue;

    public Dialog InProgressDialogue => inProgressDialogue?.Lines?.Count > 0 ? inProgressDialogue : startDialogue;

    public Dialog CompletedDialogue => completedDialogue;

    public ItemBase RequiredItem => requiredItem;

    public ItemBase RewardItem => rewardItem;
}
