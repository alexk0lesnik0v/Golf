using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Golf
{
    public class LevelController : MonoBehaviour
    {
        public event Action Finished;
        public event Action<int> HitChanged;
        
        [SerializeField] private Button m_playButton;

        [SerializeField] [Min(0)] private float m_spawnZombieRate = 5f;
        [SerializeField] [Min(0)] private float m_spawnStoneRate = 1f;
        [SerializeField] private StoneSpawner m_stoneSpawner;
        [SerializeField] private ZombieSpawner[] m_zombieSpawner;
        [SerializeField] private ScoreManager m_scoreManager;
        
        private float m_currentStoneSpawnRate;
        private float m_currentZombieSpawnRate;
        private float m_stoneTime;
        private float m_enemyTime;
        private int m_currentHitCount;
        
        public int currentHitCount
        {
            get  => m_currentHitCount;
            private set
            {
                m_currentHitCount = value;
                HitChanged?.Invoke(value);
            }
        }
       
        public void Initialize()
        {
            m_currentStoneSpawnRate = m_spawnStoneRate;
            m_currentZombieSpawnRate = m_spawnZombieRate;
        }
        
        private void Update()
        {
            m_stoneTime += Time.deltaTime;
            m_enemyTime  += Time.deltaTime;
           
            if (m_stoneTime >= m_currentStoneSpawnRate)
            {
                Stone stone = m_stoneSpawner.Spawn();
                
                stone.Hit += OnHitStone;
                stone.Missed += OnMissed;
                
                m_stoneTime = 0;
            }
            
            if (m_enemyTime >= m_currentZombieSpawnRate)
            {
                foreach (ZombieSpawner zombieSpawner in m_zombieSpawner)
                {
                    Enemy zombie = zombieSpawner.Spawn();
                }
                
                m_enemyTime = 0;
            }
        }

        private void OnHitStone(Stone stone)
        {
            UnsubscribeStone(stone);
            currentHitCount++;
            
            m_scoreManager.Increase();
        }
        
        private void OnMissed(Stone stone)
        {
            UnsubscribeStone(stone);
            
            currentHitCount = 0;
        }

        public void GameOver()
        {
            Finished?.Invoke();
        }

        private void UnsubscribeStone(Stone stone)
        {
            stone.Hit -= OnHitStone;
            stone.Missed -= OnMissed;
        }
    }
}