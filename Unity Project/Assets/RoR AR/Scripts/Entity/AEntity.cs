using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RORAR.Entity
{
    [RequireComponent(typeof(LeanSelectable))]
    [RequireComponent(typeof(Interaction))]
    public class AEntity : MonoBehaviour
    {
        [SerializeField]
        protected Interaction m_interaction;
        [SerializeField]
        protected LeanSelectable m_leanSelectable;
        [SerializeField]
        protected bool m_doesRotate;
        [SerializeField]
        protected bool m_doesScale;
        private Renderer[] m_renderers;

        public virtual void Init()
        {
            m_interaction.Init(m_doesRotate, m_doesScale, m_leanSelectable);
            gameObject.SetActive(false);
            transform.localPosition = Vector3.zero;
            m_renderers = GetComponentsInChildren<Renderer>();
        }

        public virtual void Enable(Transform a_parent)
        {
            transform.SetParent(a_parent, false);
            transform.localPosition = Vector3.zero;
            transform.rotation = a_parent.transform.rotation;
            gameObject.SetActive(true);
        }

        public virtual void Disable(Transform a_defaultParent)
        {
            transform.SetParent(a_defaultParent, false);
            transform.rotation = Quaternion.identity;
            gameObject.SetActive(false);
        }

        public virtual void CleanUp()
        {

        }

        public virtual void Manage()
        {
            m_interaction.Manage();
        }

        public void EnableRenderers(bool a_enable)
        {
            for (int i = 0; i < m_renderers.Length; ++i)
            {
                m_renderers[i].enabled = a_enable;
            }
        }
    }
}