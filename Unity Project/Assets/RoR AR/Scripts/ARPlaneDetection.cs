﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Vuforia;

public class ARPlaneDetection : MonoBehaviour
{
    public GameObject AnchorStage;
    //private PositionalDeviceTracker _deviceTracker;
    private GameObject _previousAnchor;

    public void Start()
    {
        if (AnchorStage == null)
        {
            Debug.Log("AnchorStage must be specified");
            return;
        }
        AnchorStage.SetActive(false);
    }

    //public void Awake()
    //{
    //    VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
    //}

    //public void OnDestroy()
    //{
    //    VuforiaARController.Instance.UnregisterVuforiaStartedCallback(OnVuforiaStarted);
    //}

    //private void OnVuforiaStarted()
    //{
    //    _deviceTracker = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();
    //}

    //public void OnInteractiveHitTest(HitTestResult result)
    //{
    //    if (result == null || AnchorStage == null)
    //    {
    //        Debug.LogWarning("Hit test is invalid or AnchorStage not set");
    //        return;
    //    }

    //    var anchor = _deviceTracker.CreatePlaneAnchor(Guid.NewGuid().ToString(), result);

    //    if (anchor != null)
    //    {
    //        //récupérer le model et y ajouter un anchor behaviour
    //        //appeler ConfigureANchor
    //        //set la position et rotation du modèle


    //        AnchorBehaviour anchorBehaviour = AnchorStage.GetComponent<AnchorBehaviour>();
    //        if (anchorBehaviour == null)
    //            anchorBehaviour = AnchorStage.AddComponent<AnchorBehaviour>();

    //        anchorBehaviour.ConfigureAnchor(anchor);
    //        AnchorStage.transform.position = result.Position;
    //        AnchorStage.transform.rotation = result.Rotation;
    //        AnchorStage.transform.localPosition = Vector3.zero;
    //        AnchorStage.transform.localRotation = Quaternion.identity;
    //        AnchorStage.SetActive(true);
    //    }

    //    if (_previousAnchor != null)
    //    {
    //        Destroy(_previousAnchor);
    //    }

    //    //_previousAnchor = anchor;
    //}
}