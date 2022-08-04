using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public event Action onMenuClose;
    public event Action<int> onMenuSelected;
    [SerializeField] GameObject menu;

    List<Text> menuItems;
    int selectedIndex = 0;
    Color itemColor;


    private void Awake()
    {
        menuItems = menu.GetComponentsInChildren<Text>().ToList();
        itemColor = menuItems[0].color;

    }

    public void OpenMenu()
    {
        menu.SetActive(true);
        menuItems[selectedIndex].color = GlobalSettings.Instance.HighlightedColor;
    }
    
    public void DeactivateMenu()
    {
        menu.SetActive(false);
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(false);
            onMenuClose();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex++;
            if (selectedIndex >= menuItems.Count)
            {
                selectedIndex = 0;
            }
            UpdateSelectedItem();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex--;
            if (selectedIndex < 0)
            {
                selectedIndex = menuItems.Count - 1;
            }
            UpdateSelectedItem();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            onMenuSelected(selectedIndex);
        }



    }
    
    void UpdateSelectedItem()
    {
        foreach (var item in menuItems)
        {
            item.color = GlobalSettings.Instance.DefaultColor;
        }
        menuItems[selectedIndex].color = GlobalSettings.Instance.HighlightedColor;
    }

    

}
