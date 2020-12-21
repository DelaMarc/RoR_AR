using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using UnityEngine.UI;
using Vuforia;

public class ModelManager : MonoBehaviour
{
    enum DummyPosition
    {
        straigt,
        inclined
    }

    public static ModelManager instance;
    List<GameObject> models;
    int currentSelected;
    GameObject imageTargetDummy;
    [SerializeField]
    GameObject inclinedDummy, straightDummy;
    DummyPosition dummyPosition = DummyPosition.straigt;
    GameObject currentARObject;
    [HideInInspector]
    public bool imageTargetVisible = false;
    [SerializeField]
    bool verbose;

    //initialize singleton
    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        instance = this;
        models = new List<GameObject>();
        currentSelected = -1;
        //setup the rest of the class
        imageTargetDummy = straightDummy;
        //get first AR Object assigned to Image Target        
        if (imageTargetDummy.transform.childCount == 0)
            currentARObject = null;
        else
            currentARObject = imageTargetDummy.transform.GetChild(0).gameObject;        
    }

    public void SwitchDummy(Text textButton)
    {
        //switch active dummy
        if (dummyPosition == DummyPosition.straigt)
        {
            imageTargetDummy = inclinedDummy;
            dummyPosition = DummyPosition.inclined;
            textButton.text = "View : inclined";
            if (verbose)
            {
                Debug.Log("Set dummy to inclined");
            }
        }
        else
        {
            imageTargetDummy = straightDummy;
            dummyPosition = DummyPosition.straigt;
            textButton.text = "View : straight";
            if (verbose)
            {
                Debug.Log("Set dummy to straight");
            }
        }
        //update model position
        if (currentARObject != null)
        {
            currentARObject.transform.SetParent(imageTargetDummy.transform);
            currentARObject.transform.localPosition = Vector3.zero;
            currentARObject.transform.rotation = imageTargetDummy.transform.rotation;
        }
    }

    //add a new model to the object list
    public void AddModel(GameObject model)
    {
        GameObject newModel;

        newModel = Instantiate(model, instance.transform);
        newModel.SetActive(false);
        newModel.transform.localPosition = Vector3.zero;
        models.Add(newModel);
    }

    //disable all the renderers from a model, which is necessary before placing it on image target
    public void EnableItemRenderers(int index, bool enable)
    {
        GameObject model = models[index];
        Renderer[] renderers = model.GetComponentsInChildren<Renderer>();

        if (verbose)
        {
            Debug.Log("Enable Item Renderers : " + enable);
        }
        for (int i = 0; i < renderers.Length; ++i)
        {
            renderers[i].enabled = enable;
        }
    }

    //set a selected model on vuforia image target
    public void SelectModel(int index)
    {
        GameObject newModel;

        //don't execute code if selected model is the same as before
        if (index == currentSelected)
            return;
        else
            currentSelected = index;
        //hide previous model on Image Target and reset its values
        if (currentARObject != null)
        {
            currentARObject.transform.SetParent(instance.transform, false);
            currentARObject.transform.rotation = Quaternion.identity;
            currentARObject.SetActive(false);
        }
        //place new model on image target;
        newModel = models[index];
        //disable model renderers if image target is not detected
        if (imageTargetVisible == false)
        {
            EnableItemRenderers(index, false);
        }
        //re enable renderers in case they were disabled
        else
        {
            EnableItemRenderers(index, true);
        }
        newModel.transform.SetParent(imageTargetDummy.transform, false);
        newModel.transform.localPosition = Vector3.zero;
        newModel.transform.rotation = imageTargetDummy.transform.rotation;
        newModel.SetActive(true);
        currentARObject = newModel;
    }

    public void OnTrackingLost()
    {
        Debug.Log("<color=red>Tracking lost</color>");
    }
    public void OnTrackingFound()
    {
        Debug.Log("<color=red>Tracking found</color>");
    }
}
