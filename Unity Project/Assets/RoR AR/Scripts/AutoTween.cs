using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        visible = !visible;
        if (visible)
        {
            itemToTween.SetActive(true);
            LeanTween.moveLocalX(itemToTween, 0, transitionSpeed);
        }
        else
        {
            LeanTween.moveLocalX(itemToTween, itemHiddenPosition, transitionSpeed).setOnComplete(ToggleComplete);
        }
    }

    //to be called when the item must be hidden
    void ToggleComplete()
    {
        itemToTween.SetActive(false);
    }

}
