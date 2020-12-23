using UnityEngine;

[CreateAssetMenu(fileName = "AR Entity Data")]
public class SceneConfigData : ScriptableObject
{
    [SerializeField] EntityData[] m_items;
    [SerializeField]
    [Range(0.1f, 1f)] float m_globalScale;
    [SerializeField] bool m_verbose;
    [Header("Marker detection only")]
    [SerializeField] ModelManager.DummyPosition m_dummyPosition = ModelManager.DummyPosition.straigt;

    #region Getters
    public EntityData[] Items => m_items;
    public float GlobalScale => m_globalScale;
    public bool Verbose => m_verbose;
    public ModelManager.DummyPosition DummyPosition => m_dummyPosition;
    #endregion

    public void ApplyScale()
    {
        for (int i = 0; i < m_items.Length; ++i)
        {
            m_items[i].ApplyScale(m_globalScale);
        }
    }
}
