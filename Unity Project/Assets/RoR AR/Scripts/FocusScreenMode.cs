using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusScreenMode : MonoBehaviour
{
    [SerializeField]
    ScreenOrientation screenOrientation = ScreenOrientation.LandscapeRight;

    //force screen orientation when different from the selected one
    private void Start()
    {
        Screen.orientation = screenOrientation;
    }
}
