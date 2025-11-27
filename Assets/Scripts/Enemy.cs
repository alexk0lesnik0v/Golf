using UnityEngine;
using UnityEngine.AI;

namespace Golf
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private ScoreManager m_scoreManager;
        [SerializeField] private int m_health = 1;
        
        private GameObject m_player;
        private NavMeshAgent m_agent;
      
        void Start()
        {
            this.gameObject.SetActive(true);
            m_player =  GameObject.FindGameObjectWithTag("Player");
            m_agent = GetComponent<NavMeshAgent>();
        }
        
        void Update()
        {
            m_agent.SetDestination(m_player.transform.position);

            if (m_health <= 0)
            {
                Destroy(gameObject);
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
