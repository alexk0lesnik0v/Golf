using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace Golf
{
    public class LevelController : MonoBehaviour
    {
        public event Action Finished;
        public event Action<int> HitChanged;
        
        [SerializeField] private GameObject m_trainingOver;
        [SerializeField] private Button m_playButton;
        
        [SerializeField] private int m_missedCount;
        [SerializeField] [Min(0)] private float m_spawnRate = 1.5f;
        [SerializeField] private StoneSpawner m_stoneSpawner;
        [SerializeField] private ScoreManager m_scoreManager;
        
        private float m_currentSpawnRate;
        private float m_time;
        private List<Stone> m_stones;
        private int m_currentMissedCount;
        
        private List<GameObject> m_enemies;

        private List<GameObject> m_targetBoxes;
        
        private int m_currentHitCount;
        
        public bool m_isTraining = false;
        
        public bool m_isWinner = false;
        
        private bool m_zombiesAttack =  false;

        private bool m_isSpawn = true;

        public int currentHitCount
        {
            get  => m_currentHitCount;
            private set
            {
                m_currentHitCount = value;
                HitChanged?.Invoke(value);
            }
        }
       
        private void Awake()
        {
            
        }

        public void Initialize()
        {
            m_trainingOver.SetActive(false);
            
            m_currentMissedCount = m_missedCount;
            m_currentSpawnRate = m_spawnRate;
            
            m_stones = new List<Stone>();
            m_enemies = new List<GameObject>();
            
            GameObject[] m_foundEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in m_foundEnemies)
            {
                m_enemies.Add(enemy);
                enemy.SetActive(false);
            }
            
            m_targetBoxes = new List<GameObject>();
            
            GameObject[] m_foundTargetBoxes = GameObject.FindGameObjectsWithTag("Target");
            foreach (GameObject target in m_foundTargetBoxes)
            {
                m_targetBoxes.Add(target);
                target.SetActive(true);
            }
        }
        
        private void Update()
        {
            m_time += Time.deltaTime;
           
            if (m_time >= m_currentSpawnRate && m_isSpawn)
            {
                Stone stone = m_stoneSpawner.Spawn();
                if (!stone.CompareTag("DecreaseStone"))
                {
                    m_stones.Add(stone); 
                }
                
                stone.Hit += OnHitStone;
                stone.Missed += OnMissed;
                
                m_time = 0;
            }
        }

        private void OnHitStone(Stone stone)
        {
           UnsubscribeStone(stone);
           currentHitCount++;
           
           if (stone.CompareTag("GoldStone"))
           {
               if (currentHitCount >= 3)
               {
                   m_scoreManager.GoldComboIncrease();
               }
               else m_scoreManager.BonusIncrease();
           }
           else if (stone.CompareTag("DecreaseStone"))
           {
               m_scoreManager.Decrease();
               currentHitCount = 0;
               m_currentMissedCount--;
               FinishedController();
           }
           else if (currentHitCount >= 3)
           {
               m_scoreManager.ComboIncrease();
           }
           else m_scoreManager.Increase(stone.score);
           
           if (m_stones.Count % 5 == 0)
           {
               if (m_currentSpawnRate > 0.1f)
               {
                   m_currentSpawnRate -= 0.1f;
               }
               else m_currentSpawnRate = 0.1f;
           }
           
           WinningController();
        }
        
        private void OnMissed(Stone stone)
        {
            UnsubscribeStone(stone);
            
            if (stone.CompareTag("DecreaseStone"))
            {
                Destroy(stone.gameObject);
            }
            else
            {
                m_currentMissedCount--;
                currentHitCount = 0;
            }
            
            FinishedController();
            
            WinningController();
        }
        
        private void UnsubscribeStone(Stone stone)
        {
            stone.Hit -= OnHitStone;
            stone.Missed -= OnMissed;
        }
        
        private void FinishedController()
        {
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

        private void WinningController()
        {
            if (!m_isTraining)
            {
                m_targetBoxes?.Clear();
            
                GameObject[] m_foundTargetBoxes = GameObject.FindGameObjectsWithTag("Target");
                foreach (GameObject target in m_foundTargetBoxes)
                {
                    m_targetBoxes.Add(target);
                }
            
                if (m_targetBoxes.Count == 0)
                {
                    m_isTraining = true;
                    Debug.Log("Training is over!");
                    ZombiesAttack();
                }
            }
            
            if (m_zombiesAttack)
            {
                m_enemies?.Clear();
            
                GameObject[] m_foundEnemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in m_foundEnemies)
                {
                    m_enemies.Add(enemy);
                }
            
                if (m_enemies.Count == 0)
                {
                    m_isWinner = true;
                    Debug.Log("YOU WON!");
                    Finished?.Invoke();
                
                    m_isWinner = false;
                    m_zombiesAttack =  false;
                }
            }
        }

        private void ZombiesAttack()
        {
            m_isSpawn = false;
            m_trainingOver.SetActive(true);
            m_playButton.onClick.AddListener(onClicked);
            
            foreach (var item in m_stones)
            {
                Destroy(item.gameObject);
            }
               
            m_stones.Clear();
        }
        
        private void onClicked()
        {
            m_trainingOver.SetActive(false);
            m_playButton.onClick.RemoveListener(onClicked);

            m_missedCount = 1000;
            
            m_isSpawn = true;
            
            foreach (GameObject enemy in m_enemies)
            {
                enemy.SetActive(true);
            }
            
            m_zombiesAttack =  true;
        }
    }
}