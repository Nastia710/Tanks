using UnityEngine;

namespace Tanks.Complete
{
    public class TankMineLaying : MonoBehaviour
    {
        public GameObject m_MinePrefab;                
        public float m_Cooldown = 6f;                   
        public Vector3 m_DropOffset = new Vector3(0f, 0.5f, -2f); 

        [HideInInspector] public bool m_IsComputerControlled = false;

        private float m_CooldownTimer = 0f;             
        private TankMovement m_Movement;              

        private void Start()
        {
            m_Movement = GetComponent<TankMovement>();
            if (m_Movement != null)
            {
                m_IsComputerControlled = m_Movement.m_IsComputerControlled;
            }
        }

        private void Update()
        {
            if (m_Movement != null && !m_Movement.enabled)
                return;

            if (m_CooldownTimer > 0f)
            {
                m_CooldownTimer -= Time.deltaTime;
                return;
            }

            if (m_IsComputerControlled)
            {
                AIMineLaying();
            }
            else
            {
                PlayerMineLaying();
            }
        }

        private void PlayerMineLaying()
        {
            KeyCode dropKey = KeyCode.Alpha1; // Default

            // 1 = KeyboardLeft (WASD) = P1 in menu
            // 2 = KeyboardRight (Arrows) = P2 in menu
            if (m_Movement != null)
            {
                switch (m_Movement.ControlIndex)
                {
                    case 1: 
                        dropKey = KeyCode.Alpha1; 
                        break;
                    case 2:
                        dropKey = KeyCode.RightShift; 
                        break;
                }
            }

            if (Input.GetKeyDown(dropKey))
            {
                PlaceMine();
            }
        }

        private void AIMineLaying()
        {
            if (Random.value < 0.05f * Time.deltaTime)
            {
                PlaceMine();
            }
        }

        private void PlaceMine()
        {
            if (m_MinePrefab == null)
                return;

            Vector3 dropPosition = transform.position + transform.TransformDirection(m_DropOffset);

            GameObject mine = Instantiate(m_MinePrefab, dropPosition, Quaternion.identity);

            MineExplosion mineScript = mine.GetComponent<MineExplosion>();

            if (mineScript != null)
            {
                mineScript.m_Owner = gameObject;
            }

            m_CooldownTimer = m_Cooldown;
        }
    }
}
