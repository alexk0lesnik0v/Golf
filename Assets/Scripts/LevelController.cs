using System;
using System.Collections.Generic;
using UnityEngine;


namespace Golf
{
    public class LevelController : MonoBehaviour
    {
        public event Action Finished;
        
        [SerializeField] private int m_missedCount;
        [SerializeField] [Min(0)] private float m_spawnRate = 0.5f;
        [SerializeField] private StoneSpawner m_stoneSpawner;
        [SerializeField] private ScoreManager m_scoreManager;
        
        private float m_time;
        private List<Stone> m_stones;
        private int m_currentMissedCount;
       
        private void Awake()
        {
            m_stones = new List<Stone>();
        }

        public void Initialize()
        {
            m_currentMissedCount = m_missedCount;
        }
        
        private void Update()
        {
            m_time += Time.deltaTime;
            
            if (m_time >= m_spawnRate)
            {
                Stone stone = m_stoneSpawner.Spawn();
                m_stones.Add(stone);

                stone.Hit += OnHitStone;
                stone.Missed += OnMissed;
                
                m_time = 0;
            }
        }

        private void OnHitStone(Stone stone)
        {
           UnsubscribeStone(stone);
           if (stone.CompareTag("GoldStone"))
           {
               m_scoreManager.BonusIncrease();
           }
           else if (stone.CompareTag("DecreaseStone"))
           {
               m_scoreManager.Decrease();
           }
           else m_scoreManager.Increase();
        }
        
        private void OnMissed(Stone stone)
        {
            UnsubscribeStone(stone);

            if (stone.CompareTag("DecreaseStone"))
            {
                Destroy(stone.gameObject);
            }
            else m_currentMissedCount--;
            
            if (m_currentMissedCount <= 0)
            {
               Debug.Log("Game Over");
               Finished?.Invoke();

               foreach (var item in m_stones)
               {
                   Destroy(item.gameObject);
               }
               
               m_stones.Clear();
            }
        }
        
        private void UnsubscribeStone(Stone stone)
        {
            stone.Hit -= OnHitStone;
            stone.Missed -= OnMissed;
        }
    }
}