using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class GameManager : MonoBehaviour
{
    [SerializeField] SceneConfigData m_configData;
    [SerializeField] UIManager m_uiMgr;
    [SerializeField] ModelManager m_modelMgr;

    private void Awake()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDestroy()
    {
        EnhancedTouchSupport.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_configData.ApplyScale();
        m_modelMgr.Init(m_configData);
        m_uiMgr.Init(m_configData);
    }

    // Update is called once per frame
    void Update()
    {
        m_modelMgr.Manage();
    }
}
