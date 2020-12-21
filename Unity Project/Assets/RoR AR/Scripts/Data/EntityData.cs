using UnityEngine;
using RORAR.Entity;

[CreateAssetMenu(fileName = "AR Entity Data")]
public class EntityData : ScriptableObject
{
    [SerializeField] AEntity m_item;
    [SerializeField] string m_name = "";
    [SerializeField] bool m_doesRotate;
    [SerializeField] bool m_doesScale;
    private float m_scale;

    #region Getters
    public AEntity Item => m_item;
    public string Name => m_name;
    public bool DoesRotate => m_doesRotate;
    public bool DoesScale => m_doesScale;
    public float Scale => m_scale;
    #endregion

    public void ApplyScale(float a_scale)
    {
        m_scale = a_scale;
    }
}
