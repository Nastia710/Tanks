using UnityEngine;
using UnityEngine.UI;

namespace Tanks.Complete
{
    public class HealthZone : MonoBehaviour
    {
        [Header("Healing Settings")]
        public float m_HealAmount = 50f;                   
        public float m_HealTime = 4.5f;                    

        [Header("Visual")]
        public Image m_FillImage;                           
        public Color m_EmptyColor = new Color(0.2f, 0.8f, 0.2f, 0.3f);   
        public Color m_FullColor = new Color(0.2f, 1f, 0.2f, 1f);        

        private TankHealth m_CurrentTank = null;            
        private float m_Timer = 0f;                         
        private bool m_Used = false;                        

        private void Start()
        {
            if (m_FillImage != null)
            {
                m_FillImage.fillAmount = 0f;
                m_FillImage.color = m_EmptyColor;
            }
        }

        private void Update()
        {
            if (m_Used)
                return;

            if (m_CurrentTank != null)
            {
                m_Timer += Time.deltaTime;

                float progress = Mathf.Clamp01(m_Timer / m_HealTime);
                if (m_FillImage != null)
                {
                    m_FillImage.fillAmount = progress;
                    m_FillImage.color = Color.Lerp(m_EmptyColor, m_FullColor, progress);
                }

                if (m_Timer >= m_HealTime)
                {
                    m_CurrentTank.IncreaseHealth(m_HealAmount);
                    m_Used = true;

                    Destroy(gameObject);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (m_Used)
                return;

            TankHealth health = other.GetComponent<TankHealth>();
            if (health == null)
                return;

            m_CurrentTank = health;
            m_Timer = 0f;
        }

        private void OnTriggerExit(Collider other)
        {
            if (m_Used)
                return;

            TankHealth health = other.GetComponent<TankHealth>();
            if (health != null && health == m_CurrentTank)
            {
                m_CurrentTank = null;
                m_Timer = 0f;

                if (m_FillImage != null)
                {
                    m_FillImage.fillAmount = 0f;
                    m_FillImage.color = m_EmptyColor;
                }
            }
        }
    }
}
