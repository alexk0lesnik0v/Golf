using UnityEngine;
using UnityEngine.AI;

namespace Golf
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int m_health = 3;
        
        private GameObject m_player;
        private NavMeshAgent m_agent;
        void Start()
        {
            m_player =  GameObject.FindGameObjectWithTag("Player");
            m_agent = GetComponent<NavMeshAgent>();
        }
        
        void Update()
        {
            m_agent.SetDestination(m_player.transform.position);

            if (m_health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        
        public void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<Stone>(out var stone))
            {
                m_health--;
            }
        }
    }
}
