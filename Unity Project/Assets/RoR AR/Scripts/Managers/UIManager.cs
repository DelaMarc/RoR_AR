using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //splash screen to enable on play
    [SerializeField] GameObject fadeScreen;
    [SerializeField] Text m_toggleDummyText;
    [SerializeField] GameObject panelBlockClick;
    [SerializeField] VerticalScrollList menuItem;
    [SerializeField] GameObject weatherItem;
    [SerializeField]  float menuTransitionSpeed;
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
    }

    public void Init(SceneConfigData a_data)
    {
        //enable fade screen in case it is disabled
        fadeScreen.SetActive(true);
        menuItem.Init(a_data.Items);
        //setup dummy toggle text
        if (a_data.DummyPosition == ModelManager.DummyPosition.straigt)
        {
            m_toggleDummyText.text = "View : straight";
        }
        else
        {
            m_toggleDummyText.text = "View : inclined";
        }
    }

    //toggles appearance / disappearance of the menu
    public void ToggleMenu()
    {
        menuVisible = !menuVisible;
        if (menuVisible)
        {
            menuItem.gameObject.SetActive(true);
            LeanTween.moveLocalX(menuItem.gameObject, 0, menuTransitionSpeed);
        }
        else
        {
            LeanTween.moveLocalX(menuItem.gameObject, 1300, menuTransitionSpeed).setOnComplete(ToggleMenuComplete);
        }
        //ToggleBlockClick();
    }

    //to be called when the menu must be hidden
    void ToggleMenuComplete()
    {
        menuItem.gameObject.SetActive(false);
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
