using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Create new attack")]
public class AttackBase : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] string description;
    [SerializeField] Sprite attackSprite;

    [SerializeField] int damage;


    public string Name => name;

    public string Description => description;

    public Sprite AttackSprite => attackSprite;

    public int Damage => damage;

}
