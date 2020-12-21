using UnityEngine;

[CreateAssetMenu(fileName = "AR Entity Data")]
public class SceneConfigData : ScriptableObject
{
    [SerializeField] EntityData[] m_items;
    [SerializeField]
    [Range(0.1f, 1f)] float m_globalScale;
    [SerializeField] bool m_verbose;

    #region Getters
    public EntityData[] Items => m_items;
    public float GlobalScale => m_globalScale;
    public bool Verbose => m_verbose;
    #endregion

    public void ApplyScale()
    {
        for (int i = 0; i < m_items.Length; ++i)
        {
            m_items[i].ApplyScale(m_globalScale);
        }
    }
}
