using UnityEngine;

namespace Golf
{
    public class LavelController : MonoBehaviour
    {
        [SerializeField] [Min(0)] private float m_spawnRate = 0.5f;
        [SerializeField] private StoneSpawner m_stoneSpawner;
        
        private float m_time;

        private void Start()
        {
            m_time = m_spawnRate;
        }

        private void Update()
        {
            m_time += Time.deltaTime;
            
            if (m_time >= m_spawnRate)
            {
                m_stoneSpawner.Spawn();
                m_time = 0;
            }
        }
    }
}