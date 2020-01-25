using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AutoTween : MonoBehaviour
{
    [SerializeField]
    GameObject itemToTween;
    [SerializeField]
    float transitionSpeed;
    [SerializeField]
    int itemHiddenPosition;
    bool visible = false;
    [SerializeField]
    bool disableOnStart = true;
    [SerializeField]
    //event called in case we want more actions to play before the tween animation
    UnityEvent toggleEvent;
    bool tweening = false;

    //hide the item at application start
    private void Start()
    {
        Vector3 itemPos = itemToTween.transform.localPosition;
        itemToTween.transform.localPosition = new Vector3(itemHiddenPosition, itemPos.y, itemPos.z);
        if (disableOnStart)
            itemToTween.SetActive(false);
    }

    //toggles appearance / disappearance of the item
    public void Toggle()
    {
        //call possible more actions
        toggleEvent.Invoke();
        if (tweening == false)
        {
            visible = !visible;
            tweening = true;
            if (visible)
            {
                itemToTween.SetActive(true);
                LeanTween.moveLocalX(itemToTween, 0, transitionSpeed).setOnComplete(() => tweening = false);
            }
            else
            {
                LeanTween.moveLocalX(itemToTween, itemHiddenPosition, transitionSpeed).setOnComplete(ToggleComplete);
            }
        }
    }

    //to be called when the item must be hidden
    void ToggleComplete()
    {
        tweening = false;
        itemToTween.SetActive(false);
    }

    public void ToggleGameObject(GameObject o)
    {
        //toggle the gameobject only if tno leantween animation is playing
        if (tweening == false)
        {
            o.SetActive(!o.activeSelf);
        }
    }

}
