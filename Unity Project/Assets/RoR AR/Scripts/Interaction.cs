using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using Lean.Common;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

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
    private float m_lastSwipeValue;

    public void Init(EntityData a_data, LeanSelectable a_leanSelectable)
    {
        m_lastSwipeValue = 0;
        m_doesRotate = a_data.DoesRotate;
        m_doesScale = a_data.DoesScale;
        //initialize LeanFingerFilter
        m_use = new LeanFingerFilter(true);
        m_use.Filter = LeanFingerFilter.FilterType.AllFingers;
        //mark the leanselectable as selected so we can interact with it
        leanSelectable = a_leanSelectable;
        m_use.RequiredSelectable = leanSelectable;
        leanSelectable.SelfSelected = true;
        //set minimum scale
        minScale = a_data.Scale;
        transform.localScale = Vector3.one * minScale;
        pinchScale = 1f;
    }

    public void RotateObject()
    {
        //rotate object if a swipe is detected and we are not scaling
        if (Touch.activeTouches.Count > 0 && pinchScale == 1)
        {
            var touch = Touch.activeTouches[0];
            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Moved && touch.delta.x != m_lastSwipeValue)
            {
                m_lastSwipeValue = touch.delta.x;
                float x = Mathf.Clamp(touch.delta.x, -1f, 1f);
                float rotationX = x * rotationSpeed * Mathf.Deg2Rad;
                transform.RotateAround(transform.up, -rotationX);
            }
        }
    }

    void OnMouseDrag()
    {
        
    }

    public void ScaleObject()
    {
        List<LeanFinger> fingers = m_use.UpdateAndGetFingers();
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
            leanSelectable.SelfSelected = true;
        if (m_doesScale)
            ScaleObject();
        if (m_doesRotate)
            RotateObject();
    }
}
