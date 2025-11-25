using UnityEngine;
using UnityEngine.AI;

namespace Golf
{
    public class Enemy : MonoBehaviour
    {
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
        }
    }
}
