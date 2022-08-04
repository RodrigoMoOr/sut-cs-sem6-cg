using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Create new recovery item")]

public class RecoveryItem : ItemBase
{
    
    [SerializeField] int hpAmount;
    [SerializeField] int mpAmount;

    public override void Use()
    {
        PlayerStats.Instance.AddHP(hpAmount);
        PlayerStats.Instance.AddMP(mpAmount);
    }
}
