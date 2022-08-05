using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    [SerializeField] Color defaultColor;
    [SerializeField] Color highlightedColor;
    [SerializeField] int itemsInInventoryViewport;


    public Color DefaultColor => defaultColor;

    public Color HighlightedColor => highlightedColor;

    public int ItemsInInventoryViewport => itemsInInventoryViewport;

    public static GlobalSettings Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
}
