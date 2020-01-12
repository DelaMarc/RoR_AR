using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SplashScreen : MonoBehaviour
{
    [SerializeField]
    RectTransform fade;
    [SerializeField]
    UnityEvent sceneStartEvent;
    [SerializeField]
    float switchSceneDelay;

    // Start is called before the first frame update
    void Start()
    {
        //fade out
        LeanTween.alpha(fade, 0, 1).setOnComplete(FirstFadeComplete);
    }

    void FirstFadeComplete()
    {
        sceneStartEvent.Invoke();
    }

    public void FadeToLevel()
    {
        LeanTween.alpha(fade, 1, 1).setOnComplete(OnFadeInComplete).setDelay(switchSceneDelay);
    }

    void OnFadeInComplete()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); ;
    }

    //used be event to destroy  UI elements once they become useless
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }

}
