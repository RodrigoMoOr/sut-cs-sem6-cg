using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Create new quest item")]

public class QuestItem : ItemBase
{

    public override void Use()
    {
        Debug.Log("Quest item used");
    }
}
