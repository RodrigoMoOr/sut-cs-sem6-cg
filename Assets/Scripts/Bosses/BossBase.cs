using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Bosses/Create new boss")]
public class BossBase : ScriptableObject
{

    
    [SerializeField] public string bossName;

    [TextArea]
    [SerializeField] public string bossDescription;

    [SerializeField] public int maxHealth;
    [SerializeField] public int damage;
    [SerializeField] public int defense;


    [SerializeField] Sprite bossNPCSprite;
    [SerializeField] Sprite bossBattleImage;

    

    [SerializeField] List<AttackBase> possibleAttacks;


    public Sprite NPCSprite => bossNPCSprite;
    public Sprite BattleImage => bossBattleImage;

    public List<AttackBase> PossibleAttacks => possibleAttacks;


}
