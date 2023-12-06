using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RORAR.Entity;

public class ModelManager : MonoBehaviour
{
    public enum DummyPosition
    {
        straigt,
        inclined
    }

    public static ModelManager Instance;
    List<AEntity> m_models;
    int m_currentSelected;
    GameObject m_imageTargetDummy;
    [SerializeField] GameObject m_inclinedDummy, m_straightDummy;
    DummyPosition m_dummyPosition = DummyPosition.straigt;
    AEntity m_currentARObject;
    [HideInInspector]
    public bool m_imageTargetVisible = false;
    bool m_verbose;

    public void Init(SceneConfigData a_data)
    {
        //initialize singleton
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        //set dummy position
        m_dummyPosition = a_data.DummyPosition;
        if (m_dummyPosition == DummyPosition.straigt)
            m_imageTargetDummy = m_straightDummy;
        else
            m_imageTargetDummy = m_inclinedDummy;
        //setup model list
        m_models = new List<AEntity>();
        m_currentSelected = -1;
        //setup the rest of the class
        
        //get first AR Object assigned to Image Target        
        if (m_imageTargetDummy.transform.childCount == 0)
            m_currentARObject = null;
        else
            m_currentARObject = m_imageTargetDummy.transform.GetChild(0).gameObject.GetComponent<AEntity>();
        //update Object Manager List
        for (int i = 0; i < a_data.Items.Length; ++i)
        {
            AddModel(a_data.Items[i]);
        }
        m_verbose = a_data.Verbose;
    }

    private void Log(string a_msg)
    {
        if (m_verbose)
        {
            Debug.Log(a_msg);
        }
    }

    public void SwitchDummy(Text textButton)
    {
        //switch active dummy
        if (m_dummyPosition == DummyPosition.straigt)
        {
            m_imageTargetDummy = m_inclinedDummy;
            m_dummyPosition = DummyPosition.inclined;
            textButton.text = "View : inclined";
            Log("Set dummy to inclined");
        }
        else
        {
            m_imageTargetDummy = m_straightDummy;
            m_dummyPosition = DummyPosition.straigt;
            textButton.text = "View : straight";
            Log("Set dummy to straight");
        }
        //update model position
        if (m_currentARObject != null)
        {
            m_currentARObject.transform.SetParent(m_imageTargetDummy.transform);
            m_currentARObject.transform.localPosition = Vector3.zero;
            m_currentARObject.transform.rotation = m_imageTargetDummy.transform.rotation;
        }
    }

    //add a new model to the object list
    public void AddModel(EntityData a_data)
    {
        AEntity newModel;

        newModel = Instantiate(a_data.Item, Instance.transform);
        newModel.Init(a_data);
        m_models.Add(newModel);
    }

    //disable all the renderers from a model, which is necessary before placing it on image target
    public void EnableItemRenderers(int index, bool enable)
    {
        AEntity model = m_models[index];

        Log("Enable Item Renderers : " + enable);
        model.EnableRenderers(enable);
    }

    //set a selected model on vuforia image target
    public void SelectModel(int index)
    {
        AEntity newModel;

        //don't execute code if selected model is the same as before
        if (index == m_currentSelected)
            return;
        else
            m_currentSelected = index;
        //hide previous model on Image Target and reset its values
        if (m_currentARObject != null)
        {
            m_currentARObject.Disable(transform);
        }
        //place new model on image target;
        newModel = m_models[index];
        //disable model renderers if image target is not detected
        if (m_imageTargetVisible == false)
        {
            EnableItemRenderers(index, false);
        }
        //re enable renderers in case they were disabled
        else
        {
            EnableItemRenderers(index, true);
        }
        newModel.Enable(m_imageTargetDummy.transform);
        m_currentARObject = newModel;
    }

    public void Manage()
    {
        m_currentARObject?.Manage();
    }

    #region Tracking
    public void OnTrackingFound()
    {
        Log("<color=green>OnTrackingFound</color>");
        m_imageTargetVisible = true;
    }

    public void OnTrackingLost()
    {
        m_imageTargetVisible = false;
        Log("<color=red>OnTrackingLost</color>");
    }

    #endregion
}

