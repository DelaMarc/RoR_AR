using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    //splash screen to enable on play
    GameObject fadeScreen;
    [SerializeField]
    GameObject panelBlockClick;
    [SerializeField]
    GameObject menuItem;
    [SerializeField]
    GameObject weatherItem;
    [SerializeField]
    float menuTransitionSpeed;
    bool menuVisible = false;
    bool weatherVisible = false;
    public static UIManager instance;

    //initialize singleton
    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        instance = this;
        //enable fade screen in case it is disabled
        fadeScreen.SetActive(true);
    }

    public void ToggleBlockClick()
    {
        panelBlockClick.SetActive(!panelBlockClick.activeSelf);
    }

    //toggles appearance / disappearance of the menu
    public void ToggleMenu()
    {
        menuVisible = !menuVisible;
        if (menuVisible)
        {
            menuItem.SetActive(true);
            LeanTween.moveLocalX(menuItem, 0, menuTransitionSpeed);
        }
        else
        {
            LeanTween.moveLocalX(menuItem, 1300, menuTransitionSpeed).setOnComplete(ToggleMenuComplete);
        }
        //ToggleBlockClick();
    }

    //to be called when the menu must be hidden
    void ToggleMenuComplete()
    {
        menuItem.SetActive(false);
    }

    //toggles appearance / disappearance of the menu
    public void ToggleWeather()
    {
        weatherVisible = !weatherVisible;
        if (weatherVisible)
        {
            weatherItem.SetActive(true);
            LeanTween.moveLocalX(weatherItem, 0, menuTransitionSpeed);
        }
        else
        {
            LeanTween.moveLocalX(weatherItem, 1300, menuTransitionSpeed).setOnComplete(ToggleWeatherComplete);
        }
    }

    //to be called when the menu must be hidden
    void ToggleWeatherComplete()
    {
        weatherItem.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
