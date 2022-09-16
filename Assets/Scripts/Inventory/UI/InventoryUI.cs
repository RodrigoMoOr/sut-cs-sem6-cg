using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{



    public event Action onInventoryClose;

    [SerializeField] GameObject itemList;
    [SerializeField] ItemSlotUI ItemSlotUI;
    [SerializeField] GameObject itemScrollView;

    [SerializeField] Image itemIcon;
    [SerializeField] Text itemDescription;

    private Color defaultColor;

    
    private Color highlightedColor;
    

    int selectedItem = 0;
    int itemsInViewport;

    List<ItemSlotUI> slotUIList;

    Inventory inventory;
    
    

    RectTransform itemListRect;

    private void Awake()
    {
        inventory = Inventory.GetInventory();
        itemListRect = itemList.GetComponent<RectTransform>();


    }

    private void Start()
    {
        inventory.onUpdated += () => UpdateItemList();

        defaultColor = GlobalSettings.Instance.DefaultColor;
        highlightedColor = GlobalSettings.Instance.HighlightedColor;
        UpdateItemList();
        
        //How many items are visible at once
        itemsInViewport = (int)(itemScrollView.GetComponent<RectTransform>().sizeDelta.y / slotUIList[0].Height);
    }


    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onInventoryClose();
        }


        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedItem++;
            if (selectedItem >= inventory.Slots.Count)
            {
                selectedItem = 0;
            }
            UpdateSelectedItem();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedItem--;
            if (selectedItem < 0)
            {
                selectedItem = inventory.Slots.Count - 1;
            }
            UpdateSelectedItem();


        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            inventory.UseItem(selectedItem);
        }

    }



    void UpdateItemList()
    {
        // Clear all the existing items
        foreach (Transform child in itemList.transform)
            Destroy(child.gameObject);

        slotUIList = new List<ItemSlotUI>();
        foreach (var itemSlot in Inventory.GetInventory().Slots)
        {
            var slotUIObj = Instantiate(ItemSlotUI, itemList.transform);
            slotUIObj.SetData(itemSlot);

            slotUIList.Add(slotUIObj);
        }
        if(selectedItem > Inventory.GetInventory().Slots.Count - 1)
        {
            selectedItem--;
        }
        UpdateSelectedItem();
    }


    void UpdateSelectedItem()
    {
        foreach (var slot in slotUIList)
        {
            slot.NameText.color = defaultColor;
            //item.NameText.color = Color.black;
        }
        //slotUIList[selectedItem].NameText.color = Color.yellow;
        slotUIList[selectedItem].NameText.color = highlightedColor;


        var item = inventory.Slots[selectedItem].Item;
        itemIcon.sprite = item.Icon;
        itemDescription.text = item.Description;


        HandleScrolling();

    }

    void HandleScrolling()
    {
        itemListRect.localPosition = new Vector3(0, slotUIList[0].Height * (selectedItem + itemsInViewport/2), 0);

    }
}