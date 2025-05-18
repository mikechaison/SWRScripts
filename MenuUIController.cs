using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour
{
    public List<Button> Buttons; 
    public Canvas MainMenu;
    public Canvas About;
    public Canvas ChooseLevel;
    int maxLvl;

    public void Start()
    {
        maxLvl = PlayerPrefs.GetInt("MaxLvl", 1);
        for (int i = 0; i < Buttons.Count; i++)
        {
            if (i < maxLvl)
            {
                Buttons[i].interactable = true;
            }
            else
            {
                Buttons[i].interactable = false;
            }
        }
    }

    public void GoAbout()
    {
        About.enabled = true;
        MainMenu.enabled = false;
        ChooseLevel.enabled = false;
    }

    public void GoChoose()
    {
        ChooseLevel.enabled = true;
        MainMenu.enabled = false;
        About.enabled = false;
    }

    public void GoBack()
    {
        MainMenu.enabled = true;
        About.enabled = false;
        ChooseLevel.enabled = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
