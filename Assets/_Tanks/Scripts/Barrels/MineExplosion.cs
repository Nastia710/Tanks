using UnityEngine;

namespace Tanks.Complete
{
    public class MineExplosion : MonoBehaviour
    {
        public LayerMask m_TankMask;                        
        public GameObject m_ExplosionPrefab;                
        
        public float m_MaxDamage = 80f;                     
        public float m_ExplosionForce = 1200f;              
        public float m_ExplosionRadius = 5f;                
        public float m_ArmingDelay = 1.5f;                  
        [HideInInspector] public GameObject m_Owner;       

        private bool m_Exploded = false;
        private float m_Timer = 0f;
        private bool m_Armed = false;

        private void Update()
        {
            if (!m_Armed)
            {
                m_Timer += Time.deltaTime;
                if (m_Timer >= m_ArmingDelay)
                {
                    m_Armed = true;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (m_Exploded || !m_Armed)
                return;

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb == null)
                return;

            TankHealth health = rb.GetComponent<TankHealth>();
            if (health == null)
                return;

            Explode();
        }

        private void Explode()
        {
            m_Exploded = true;

            Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);

            for (int i = 0; i < colliders.Length; i++)
            {
                Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

                if (!targetRigidbody)
                    continue;

                TankMovement tankMovement = targetRigidbody.GetComponent<TankMovement>();
                if (tankMovement != null)
                {
                    tankMovement.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);
                }

                TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

                if (!targetHealth)
                    continue;

                float damage = CalculateDamage(targetRigidbody.position);
                targetHealth.TakeDamage(damage);
            }

            if (m_ExplosionPrefab != null)
            {
                GameObject explosionInstance = Instantiate(m_ExplosionPrefab, transform.position, transform.rotation);
                
                ParticleSystem ps = explosionInstance.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    ps.Play();
                }

                Destroy(explosionInstance, 2.0f);
            }

            Destroy(gameObject);
        }

        private float CalculateDamage(Vector3 targetPosition)
        {
            Vector3 explosionToTarget = targetPosition - transform.position;
            float explosionDistance = explosionToTarget.magnitude;
            float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;
            float damage = relativeDistance * m_MaxDamage;
            damage = Mathf.Max(0f, damage);
            return damage;
        }
    }
}
