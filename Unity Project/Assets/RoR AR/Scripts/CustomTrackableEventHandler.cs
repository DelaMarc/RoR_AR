using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CustomTrackableEventHandler : DefaultTrackableEventHandler
{
    [SerializeField]
    bool verbose;

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        if (ModelManager.instance != null)
        {
            ModelManager.instance.imageTargetVisible = true;
            if (verbose)
            {
                Debug.Log("Traacking found");
            }
        }
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        if (ModelManager.instance != null)
        {
            ModelManager.instance.imageTargetVisible = false;
            if (verbose)
            {
                Debug.Log("Traacking lost");
            }
        }
    }
}
