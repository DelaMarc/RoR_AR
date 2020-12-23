using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class Interaction : MonoBehaviour
{
    LeanFingerFilter m_use;
    LeanSelectable leanSelectable;
    [SerializeField]
    float maxScale;
    float minScale;
    float pinchScale;
    [SerializeField]
    float rotationSpeed = 5f;

    private bool m_doesRotate = true;
    private bool m_doesScale = true;

    public void Init(EntityData a_data, LeanSelectable a_leanSelectable)
    {
        m_doesRotate = a_data.DoesRotate;
        m_doesScale = a_data.DoesScale;
        //initialize LeanFingerFilter
        m_use = new LeanFingerFilter(true);
        m_use.Filter = LeanFingerFilter.FilterType.AllFingers;
        //mark the leanselectable as selected so we can interact with it
        leanSelectable = a_leanSelectable;
        m_use.RequiredSelectable = leanSelectable;
        leanSelectable.IsSelected = true;
        //set minimum scale
        minScale = a_data.Scale;
        transform.localScale = Vector3.one * minScale;
        pinchScale = 1f;
    }

    public void RotateObject()
    {
        //rotate object if a swipe is detected and we are not scaling
        if (Input.touchCount > 0 && pinchScale == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                float rotationX = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
                transform.RotateAround(transform.up, -rotationX);
                //transform.Rotate(transform.up * rotationX);
            }
        }
    }

    void OnMouseDrag()
    {
        
    }

    public void ScaleObject()
    {
        List<LeanFinger> fingers = m_use.GetFingers();
        pinchScale =  Mathf.Clamp(LeanGesture.GetPinchScale(fingers), 0, 2f);

        if (pinchScale != 1f)
        {
            //update local scale 
            transform.localScale *= pinchScale;
            if (transform.localScale.x > maxScale)
                transform.localScale = new Vector3(maxScale, maxScale, maxScale);
            if (transform.localScale.x < minScale)
                transform.localScale = new Vector3(minScale, minScale, minScale);
        }
    }

    public void Manage()
    {
        if (leanSelectable.IsSelected == false)
            leanSelectable.IsSelected = true;
        if (m_doesScale)
            ScaleObject();
        if (m_doesRotate)
            RotateObject();
    }
}
