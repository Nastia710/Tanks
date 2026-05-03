using UnityEngine;

namespace Tanks.Complete
{
    public class ExplosiveBarrel : MonoBehaviour
    {
        public LayerMask m_TankMask;                        
        public GameObject m_ExplosionPrefab;                
        
        public float m_MaxDamage = 100f;                    
        public float m_ExplosionForce = 1000f;              
        public float m_ExplosionRadius = 5f;                

        private bool m_Exploded = false;                    

        private void OnTriggerEnter(Collider other)
        {
            if (!m_Exploded && other.GetComponent<ShellExplosion>() != null)
            {
                Explode();
            }
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
                Instantiate(m_ExplosionPrefab, transform.position, transform.rotation);
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
