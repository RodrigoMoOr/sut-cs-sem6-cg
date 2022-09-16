using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiver : MonoBehaviour, Interactable
{
    [SerializeField] ItemBase item;
    [SerializeField] Dialog dialog;
    bool given = false;

    public IEnumerator Interact(Transform player)
    {
        if (!Inventory.GetInventory().CheckIfExists(item))
        {
            yield return DialogManager.Instance.ShowDialog(dialog);
            player.GetComponent<Inventory>().AddItem(item);
            given = true;
        }
    }
}
