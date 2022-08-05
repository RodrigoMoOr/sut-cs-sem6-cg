using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState {FreeRoam, Battle, Dialog, Menu, Bag}


public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    [SerializeField] InventoryUI inventoryUI;
    Boss boss;


    MenuController menuController;
    GameState gameState;


    private void Start()
    {
        
        playerController.onEncounter += boss =>
        {
            gameState = GameState.Battle;
            battleSystem.gameObject.SetActive(true);

            battleSystem.SetBossData(boss);
            
            worldCamera.gameObject.SetActive(false);
        };
        
        battleSystem.OnBattleQuit += ()=> {
            gameState = GameState.FreeRoam;
            battleSystem.gameObject.SetActive(false);
            worldCamera.gameObject.SetActive(true);
        };

        DialogManager.Instance.onDialogStart += () => gameState = GameState.Dialog;

        DialogManager.Instance.onDialogEnd += () =>
        {
            if (gameState == GameState.Dialog)
                gameState = GameState.FreeRoam;
        };

        menuController.onMenuClose += () => gameState = GameState.FreeRoam;

        menuController.onMenuSelected += OnMenuSelected;

        inventoryUI.onInventoryClose += () => {
            gameState = GameState.Menu;
            menuController.OpenMenu();
            inventoryUI.gameObject.SetActive(false);
        };
    }


    private void Awake()
    {
        menuController = GetComponent<MenuController>();


        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        
    }
    
    private void Update()
    {
        if (gameState == GameState.FreeRoam)
        {
            playerController.HandleUpdate();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuController.OpenMenu();
                gameState = GameState.Menu;
            }


        }

        else if (gameState == GameState.Battle)
        {
            battleSystem.HandleUpdate();
        }
        else if (gameState == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
        else if (gameState == GameState.Menu)
        {
            menuController.HandleUpdate();
        }
        else if (gameState == GameState.Bag)
        {
            inventoryUI.HandleUpdate();
        }

    }





    void OnMenuSelected(int selection)
    {
        if(selection == 0)
        {
            inventoryUI.gameObject.SetActive(true);
            menuController.DeactivateMenu();
            gameState = GameState.Bag;
            Debug.Log("Inventory");
        }
        else if (selection == 1)
        {
            Debug.Log("Save");
        }
        else if (selection == 2)
        {
            Debug.Log("Load");
        }
        else if (selection == 3)
        {
            Debug.Log("Exit");
            SceneManager.LoadScene("MainMenu");
        }
    }

}
