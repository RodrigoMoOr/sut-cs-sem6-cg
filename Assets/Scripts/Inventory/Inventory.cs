using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{


    public event Action onUpdated;
    
    
    [SerializeField] List<ItemSlot> slots;

    public List<ItemSlot> Slots => slots;

    public static Inventory GetInventory()
    {
        return FindObjectOfType<PlayerController>().GetComponent<Inventory>();
    }
    
    public ItemBase UseItem(int index)
    {
        var item = slots[index].Item;
        item.Use();
        Debug.Log($"used item {item.GetType()}:{item.Name}");
        RemoveItem(item);
        onUpdated?.Invoke();
        return item;
    }

    public void RemoveItem(ItemBase item)
    {
        var itemSlot = slots.First(slots => slots.Item == item);
        itemSlot.Count--;
        if (itemSlot.Count == 0)
        {
            slots.Remove(itemSlot);
        }
        
    }

    public void AddItem(ItemBase item, int count = 1)
    {

        var itemSlot = slots.FirstOrDefault(slot => slot.Item == item);
        if (itemSlot != null)
        {
            itemSlot.Count += count;
        }
        else
        {
            slots.Add(new ItemSlot()
            {
                Item = item,
                Count = count
            });
        }
        onUpdated?.Invoke();
    }
  
}


[Serializable]
public class ItemSlot
{
    [SerializeField]ItemBase item;
    [SerializeField]int count;


    public ItemBase Item
    {
        get => item;
        set => item = value;
    }

    public int Count
    {
        get => count;
        set => count = value;
    }
}
