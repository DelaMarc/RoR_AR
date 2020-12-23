using UnityEngine;

namespace RORAR.Entity
{
    public class MermaidEntity : AEntity
    {
        private int ATTACK_ANIM = Animator.StringToHash("attack");
        [Header("Mermaid behaviour")]
        [SerializeField] Animator m_animator;
        [SerializeField] float m_engageDist = 2f;
        [SerializeField] bool m_followCamera = false;
        Vector3 m_camPos;
        bool m_isAttacking;

        #region Lifecycle methods
        public override void Init(EntityData a_data)
        {
            base.Init(a_data);
            m_camPos = Camera.main.transform.position;
            transform.rotation = Quaternion.identity;
        }

        public override void Manage()
        {
            base.Manage();
            Vector3 camPos = Camera.main.transform.position;
            if (m_camPos != camPos)
            {
                float dist = GetDist();
                //Debug.Log($"<color>dist:{dist}, engagement dist:{m_engageDist}, attacking:{m_isAttacking}</color>");
                //we are close enough to be attacked
                if (dist <= m_engageDist && m_isAttacking == false)
                {
                    m_isAttacking = true;
                    m_animator.SetBool(ATTACK_ANIM, true);
                }
                //we were being attacked but are now far enough
                if (dist > m_engageDist && m_isAttacking == true)
                {
                    m_isAttacking = false;
                    m_animator.SetBool(ATTACK_ANIM, false);
                }
                if (m_followCamera)
                {
                    //constantly look at the main camera
                    LookAtCamera(camPos);
                }
            }
        }
        #endregion

        void LookAtCamera(Vector3 camPos)
        {
            Transform parent = transform.parent ?? transform;
            transform.LookAt(new Vector3(camPos.x, transform.position.y, camPos.z), parent.up);
        }
        float GetDist()
        {
            Vector2 cam = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.z);
            Vector2 center = new Vector2(transform.position.x, transform.position.z);
            return Vector2.Distance(cam, center);
        }
    }
}