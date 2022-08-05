using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Moves/Create new player move")]
public class MoveBase : ScriptableObject
{

[SerializeField] string name;
[SerializeField] string description;
[SerializeField] int damage;
[SerializeField] int manaCost;

public string Name => name;
public string Description => description;
public int Damage => damage;
public int ManaCost => manaCost;
}
