using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public List<Text> textList;
    private int highlightedOption = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            incrementOption();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            decrementOption();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            selectOption();
        }


        highlightOption();
    }


    void incrementOption()
    {
        highlightedOption++;
        if (highlightedOption >= textList.Count)
        {
            highlightedOption = 0;
        }
    }


    void decrementOption()
    {
        highlightedOption--;
        if (highlightedOption < 0)
        {
            highlightedOption = textList.Count - 1;
        }
    }
    

    void selectOption()
    {
        switch (highlightedOption)
        {
            case 0:
                SceneManager.LoadScene("SampleScene");
                //Start Game
                break;
            case 1:
                Debug.Log("Quit");
                //Quit
                break;
        }
    }

    void highlightOption()
    {
        for (int i = 0; i < textList.Count; ++i)
        {
            if (i == highlightedOption)
            {
                textList[i].color = Color.white;
            }
            else
            {
                textList[i].color = Color.red;
            }
        }
    }




}
