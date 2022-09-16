using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Moves/Create new player move")]
public class MoveBase : ScriptableObject
{

[SerializeField] new string name;
[SerializeField] string description;
[SerializeField] int damage;
[SerializeField] int manaCost;
[SerializeField] Sprite moveSprite;

public string Name => name;
public string Description => description;
public int Damage => damage;
public int ManaCost => manaCost;
public Sprite MoveSprite => moveSprite;
}
