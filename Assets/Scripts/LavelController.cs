using System;
using UnityEngine;

namespace Golf
{
    public class LavelController : MonoBehaviour
    {
        [SerializeField] private int m_missedCount;
        [SerializeField] [Min(0)] private float m_spawnRate = 0.5f;
        [SerializeField] private StoneSpawner m_stoneSpawner;
        
        private float m_time;
        private int m_currentMissedCount;

        private void Awake()
        {
            
        }

        private void Start()
        {
            m_time = m_spawnRate;
        }

        private void Update()
        {
            m_time += Time.deltaTime;
            
            if (m_time >= m_spawnRate)
            {
                StoneController stone = m_stoneSpawner.Spawn();

                stone.Hit += OnHitStone;
                stone.Missed += OnMissed;
                
                m_time = 0;
            }
        }

        private void OnHitStone(StoneController stone)
        {
            stone.Hit -= OnHitStone;
            stone.Missed -= OnMissed;
            
            Debug.Log("Score");
        }
        
        private void OnMissed(StoneController stone)
        {
            stone.Hit -= OnHitStone;
            stone.Missed -= OnMissed;

            m_currentMissedCount--;
            if (m_currentMissedCount <= 0)
            {
                Debug.Log("Game Over");
            }
        }
    }
}