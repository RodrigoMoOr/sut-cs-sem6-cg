using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] string description;
    [SerializeField] bool usable;
    [SerializeField] Sprite icon;


    public string Name => name;
    public string Description => description;
    public Sprite Icon => icon;

    public bool Usable => usable;

    public abstract void Use();
}
